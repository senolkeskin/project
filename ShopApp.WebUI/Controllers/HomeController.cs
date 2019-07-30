using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Abstract;
using ShopApp.WebUI.Models;

namespace ShopApp.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private ITicketService _ticketService;
        public HomeController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        public IActionResult Index()
        {
            return View(new TicketListModel() { Tickets=_ticketService.GetAll()});
        }
    }
}