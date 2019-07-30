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
    public class EfCoreTicketDal : EfCoreGenericRepository<Ticket, ShopContext>, ITicketDal
    {
        public Ticket GetByIdWithCategories(int id)
        {
            using (var context = new ShopContext())
            {
                return context.Tickets
                        .Where(i => i.Id == id)
                        .Include(i => i.TicketCategories)
                        .ThenInclude(i => i.Category)
                        .FirstOrDefault();
            }
        }

        public int GetCountByCategory(string category)
        {
            using (var context = new ShopContext())
            {
                var tickets = context.Tickets.AsQueryable();

                if (!string.IsNullOrEmpty(category))
                {
                    tickets = tickets
                                .Include(i => i.TicketCategories)
                                .ThenInclude(i => i.Category)
                                .Where(i => i.TicketCategories.Any(a => a.Category.Name.ToLower() == category.ToLower()));
                }
                return tickets.Count();
            }
        }

        public Ticket GetTicketDetails(int id)
        {
            using (var context=new ShopContext())
            {
                return context.Tickets
                    .Where(i => i.Id == id)
                    .Include(i => i.TicketCategories)
                    .ThenInclude(i => i.Category)
                    .FirstOrDefault();
            }
        }

        public List<Ticket> GetTicketsByCategories(string category, int page, int pageSize)
        {
            using (var context = new ShopContext())
            {
                var tickets = context.Tickets.AsQueryable();

                if (!string.IsNullOrEmpty(category))
                {
                    tickets = tickets
                                .Include(i => i.TicketCategories)
                                .ThenInclude(i => i.Category)
                                .Where(i => i.TicketCategories.Any(a => a.Category.Name.ToLower() == category.ToLower()));
                }
                return tickets.Skip((page-1)*pageSize).Take(pageSize).ToList();
            }
        }

        public void Update(Ticket entity, int[] categoryIds)
        {
            using (var context = new ShopContext())
            {
                var product = context.Tickets
                                   .Include(i => i.TicketCategories)
                                   .FirstOrDefault(i => i.Id == entity.Id);

                if (product != null)
                {
                    product.To = entity.To;
                    product.From = entity.From;
                    product.Description = entity.Description;
                    product.ImageUrl = entity.ImageUrl;
                    product.Price = entity.Price;

                    product.TicketCategories = categoryIds.Select(catid => new TicketCategory()
                    {
                        CategoryId = catid,
                        TicketId = entity.Id
                    }).ToList();

                    context.SaveChanges();
                }
            }
        }
    }
}
