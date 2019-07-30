using Microsoft.EntityFrameworkCore;
using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopApp.DataAccess.Concrete.EfCore
{
    public static class SeedDatabase
    {
        public static void Seed()
        {
            var context = new ShopContext();
            if (context.Database.GetPendingMigrations().Count() == 0)
            {
                if (context.Categories.Count() == 0)
                {
                    context.Categories.AddRange(Categories);
                }

                if (context.Tickets.Count() == 0)
                {
                    context.Tickets.AddRange(Tickets);
                    context.AddRange(TicketCategory);
                }

                context.SaveChanges();
            }
        }
        private static Category[] Categories =  {
            new Category() { Name = "THY" },
            new Category { Name = "PEGASUS" },
            new Category{Name="Onur Air" } };
        private static Ticket[] Tickets =
        {
            new Ticket(){ To="İzmir",From="Adana", Price=2000, ImageUrl="8.jpg",Description="<p>Kalkış saati:15.00 İniş saati:17.00</p>"},
            new Ticket(){ To="İzmir",From="Samsun", Price=3000, ImageUrl="8.jpg",Description="<p>Kalkış saati:15.00 İniş saati:17.00</p>"},
            new Ticket(){ To="İzmir",From="Ankara", Price=4000, ImageUrl="8.jpg",Description="<p>Kalkış saati:15.00 İniş saati:17.00</p>"},
            new Ticket(){ To="Ankara",From="Malatya", Price=5000, ImageUrl="8.jpg",Description="<p>Kalkış saati:15.00 İniş saati:17.00</p>"},
            new Ticket(){ To="Denizli",From="İzmir", Price=6000, ImageUrl="8.jpg",Description="<p>Kalkış saati:15.00 İniş saati:17.00</p>"},
            new Ticket(){ To="Istanbul",From="Ankara", Price=4000, ImageUrl="8.jpg",Description="<p>Kalkış saati:15.00 İniş saati:17.00</p>"},
            new Ticket(){ To="Ankara",From="Mardin", Price=5000, ImageUrl="8.jpg",Description="<p>Kalkış saati:15.00 İniş saati:17.00</p>"}
        };

            private static TicketCategory[] TicketCategory =
        {
            new TicketCategory() { Ticket= Tickets[0],Category= Categories[0]},
            new TicketCategory() { Ticket= Tickets[1],Category= Categories[1]},
            new TicketCategory() { Ticket= Tickets[2],Category= Categories[2]},
            new TicketCategory() { Ticket= Tickets[3],Category= Categories[1]},
            new TicketCategory() { Ticket= Tickets[4],Category= Categories[0]},
            new TicketCategory() { Ticket= Tickets[5],Category= Categories[2]},
            new TicketCategory() { Ticket= Tickets[6],Category= Categories[1]}
        };
    }
}
