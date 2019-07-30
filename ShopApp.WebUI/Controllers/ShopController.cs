using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Abstract;
using ShopApp.Entities;
using ShopApp.WebUI.Models;

namespace ShopApp.WebUI.Controllers
{
    public class ShopController : Controller
    {
        private ITicketService _ticketService;
        public ShopController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Ticket ticket = _ticketService.GetTicketDetails((int)id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(new TicketDetailsModel()
            {
                Ticket=ticket,
                Categories=ticket.TicketCategories.Select(i=>i.Category).ToList()
            });
        }
        public IActionResult List(string category,int page=1)
        {
            const int pageSize = 3;
            return View(new TicketListModel()
            {
                PagingInfo = new PagingInfo()
                {
                    TotalItems=_ticketService.GetCountByCategory(category),
                    CurrentPage=page,
                    ItemsPerPage=pageSize,
                    CurrentCategory=category
                },
                Tickets = _ticketService.GetTicketsByCategory(category,page,pageSize)
            });
        }
    }
}