using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.Entities
{
   public class TicketCategory
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; } 
        public int TicketId { get; set; }
        public Ticket Ticket { get; set; }
    }
}
