using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI.Models
{
    public class TicketDetailsModel
    {
        public Ticket Ticket { get; set; }

        public List<Category> Categories { get; set; }
    }
}
