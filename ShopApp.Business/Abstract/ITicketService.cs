using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ShopApp.Business.Abstract
{
    public interface ITicketService:IValidator<Ticket>
    {
        Ticket GetById(int id);
        Ticket GetTicketDetails(int id);
        List<Ticket> GetAll();
        List<Ticket> GetTicketsByCategory(string category,int page,int pageSize);
        bool Create(Ticket entity);
        void Update(Ticket entity);
        void Delete(Ticket entity);
        int GetCountByCategory(string category);
        Ticket GetByIdWithCategories(int id);
        void Update(Ticket entity, int[] categoryIds);
    }
}
