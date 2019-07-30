using ShopApp.Business.Abstract;
using ShopApp.DataAccess.Abstract;
using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShopApp.Business.Concrete
{
    public class TicketManager : ITicketService
    {
        private ITicketDal _ticketDal;

        public TicketManager(ITicketDal ticketDal)
        {
            _ticketDal = ticketDal;
        }

        

        public bool Create(Ticket entity)
        {
            if (Validate(entity))
            {
                _ticketDal.Create(entity);
                return true;
            }
            return false;
            _ticketDal.Create(entity);
        }

        public void Delete(Ticket entity)
        {
            _ticketDal.Delete(entity);
        }

        public List<Ticket> GetAll()
        {
            return _ticketDal.GetAll();
        }

        public Ticket GetById(int id)
        {
            return _ticketDal.GetById(id);
        }

        public Ticket GetByIdWithCategories(int id)
        {
            return _ticketDal.GetByIdWithCategories(id);
        }

        public int GetCountByCategory(string category)
        {
            return _ticketDal.GetCountByCategory(category);
        }

        public Ticket GetTicketDetails(int id)
        {
            return _ticketDal.GetTicketDetails(id);
        }

        public List<Ticket> GetTicketsByCategory(string category, int page, int pageSize)
        {
            return _ticketDal.GetTicketsByCategories(category,page,pageSize);
        }

        public void Update(Ticket entity)
        {
            _ticketDal.Update(entity);
        }

        public void Update(Ticket entity, int[] categoryIds)
        {
            _ticketDal.Update(entity, categoryIds);
        }

        public string ErrorMessage { get; set; }
        public bool Validate(Ticket entity)
        {
            var isValid = true;

            if (string.IsNullOrEmpty(entity.From))
            {
                ErrorMessage += "Gidilecek yeri girmelisiniz.";
                isValid = false;
            }
            if (string.IsNullOrEmpty(entity.To))
            {
                ErrorMessage += "Uçağa bineceğiniz yeri girmelisiniz.";
                isValid = false;
            }

            return isValid;
        }
    }
}
