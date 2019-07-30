using ShopApp.Business.Abstract;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.Business.Concrete
{
    public class CartManager : ICartService
    {
        private ICartDal _cartDal;
        public CartManager(ICartDal cartDal)
        {
            _cartDal = cartDal;
        }

        public void AddToCart(string userId, int ticketId, int quantity)
        {
            var cart = GetCartByUserId(userId);
            if (cart != null)
            {
                var index = cart.CartItems.FindIndex(i => i.TicketId == ticketId);

                if (index < 0)
                {
                    cart.CartItems.Add(new CartItem()
                    {
                        TicketId = ticketId,
                        Quantity = quantity,    
                        CartId = cart.Id
                    });
                }
                else
                {
                    cart.CartItems[index].Quantity += quantity;
                }

                _cartDal.Update(cart);
            }
        }

        public void ClearCart(string cartId)
        {
            _cartDal.CleartCart(cartId);
        }

        public void DeleteFromCart(string userId, int ticketId)
        {
            var cart = GetCartByUserId(userId);
            if (cart != null)
            {
                _cartDal.DeleteFromCart(cart.Id, ticketId);
            }
        }

        public Cart GetCartByUserId(string userId)
        {
            return _cartDal.GetByUserId(userId);
        }

        public void InitializeCart(string userId)
        {
            _cartDal.Create(new Cart() { UserId = userId });
        }
    }
}
