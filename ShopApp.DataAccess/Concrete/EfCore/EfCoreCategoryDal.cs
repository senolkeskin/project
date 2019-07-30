using Microsoft.EntityFrameworkCore;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ShopApp.DataAccess.Concrete.EfCore
{
    public class EfCoreCategoryDal : EfCoreGenericRepository<Category, ShopContext>, ICategoryDal
    {
        public void DeleteFromCategory(int categoryId, int ticketId)
        {
            using (var context = new ShopContext())
            {
                var cmd = @"delete from TicketCategory where TicketId=@p0 And CategoryId=@p1";
                context.Database.ExecuteSqlCommand(cmd, ticketId, categoryId);
            }
        }

        public Category GetByIdWithTickets(int id)
        {
            using (var context = new ShopContext())
            {
                return context.Categories
                        .Where(i => i.Id == id)
                        .Include(i => i.TicketCategories)
                        .ThenInclude(i => i.Ticket)
                        .FirstOrDefault();
            }
        }
    }
}
