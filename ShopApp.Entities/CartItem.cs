using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.Entities
{
    public class CartItem
    {
        public int Id { get; set; }

        public Ticket Ticket { get; set; }
        public int TicketId { get; set; }

        public Cart Cart { get; set; }
        public int CartId { get; set; }

        public int Quantity { get; set; }
    }
}
