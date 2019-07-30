using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ShopApp.DataAccess.Abstract
{
   public interface ITicketDal:IRepository<Ticket>
    {
        List<Ticket> GetTicketsByCategories(string category , int page, int pageSize);
        Ticket GetTicketDetails(int id);
        int GetCountByCategory(string category);
        Ticket GetByIdWithCategories(int id);
        void Update(Ticket entity, int[] categoryIds);
    }
}
