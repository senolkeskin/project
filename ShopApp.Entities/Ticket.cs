using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.Entities
{
   public class Ticket
    {
        public int Id { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public List<TicketCategory> TicketCategories { get; set; }
    }
}
