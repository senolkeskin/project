using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopApp.DataAccess.Concrete.EfCore
{
    public class EfCoreCartDal : EfCoreGenericRepository<Cart, ShopContext>, ICartDal
    {
        public override void Update(Cart entity)
        {
            using (var context = new ShopContext())
            {
                context.Carts.Update(entity);
                context.SaveChanges();
            }
        }
        public Cart GetByUserId(string userId)
        {
            using (var context = new ShopContext())
            {
                return context
                            .Carts
                            .Include(i => i.CartItems)
                            .ThenInclude(i => i.Ticket)
                            .FirstOrDefault(i => i.UserId == userId);
            }
        }

        public void DeleteFromCart(int cartId, int ticketId)
        {
            using (var context = new ShopContext())
            {
                var cmd = @"delete from CartItem where CartId=@p0 And TicketId=@p1";
                context.Database.ExecuteSqlCommand(cmd, cartId, ticketId);
            }
        }

        public void CleartCart(string cartId)
        {
            using (var context = new ShopContext())
            {
                var cmd = @"delete from CartItem where CartId=@p0";
                context.Database.ExecuteSqlCommand(cmd, cartId);
            }
        }
    }
}
