using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Business.Abstract;
using ShopApp.Entities;
using ShopApp.WebUI.Models;

namespace ShopApp.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private ITicketService _ticketService;
        private ICategoryService _categoryService;
        public AdminController(ITicketService ticketService, ICategoryService categoryService)
        {
            _ticketService = ticketService;
            _categoryService = categoryService;
        }
        public IActionResult TicketList()
        {
            return View(new TicketListModel()
            {
                Tickets = _ticketService.GetAll()
            });
        }
        [HttpGet]
        public IActionResult CreateTicket()
        {
            return View(new TicketModel());
        }
        [HttpPost]
        public IActionResult CreateTicket(TicketModel model)
        {
            if (ModelState.IsValid == true)
            {
                var entity = new Ticket()
                {
                    To = model.To,
                    From = model.From,
                    Price = model.Price,
                    Description = model.Description,
                    ImageUrl = model.ImageUrl,

                };
                if (_ticketService.Create(entity))
                {
                    return RedirectToAction("TicketList");
                }
                ViewBag.ErrorMessage = _ticketService.ErrorMessage;
                return View(model);
                
            }
            return View(model);
            
        }

        public IActionResult EditTicket(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var entity = _ticketService.GetByIdWithCategories((int)id);
            if (entity == null)
            {
                return NotFound();
            }
            var model = new TicketModel()
            {
                Id = entity.Id,
                To = entity.To,
                From = entity.From,
                Description = entity.Description,
                ImageUrl = entity.ImageUrl,
                Price = entity.Price,
                SelectedCategories = entity.TicketCategories.Select(i => i.Category).ToList()
            };
            ViewBag.Categories = _categoryService.GetAll();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditTicket(TicketModel model,int[] categoryIds,IFormFile file)
        {
            if (ModelState.IsValid)
            {
   
                var entity = _ticketService.GetById(model.Id);

                if (entity == null)
                {
                    return NotFound();
                }

                entity.To = model.To;
                entity.From = model.From;
                entity.Description = model.Description;
                entity.Price = model.Price;

                if (file != null)
                {
                    entity.ImageUrl = file.FileName;
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img", file.FileName);
                    using(var stream = new FileStream(path,FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                }

                _ticketService.Update(entity,categoryIds);

                return RedirectToAction("TicketList");
            }
            ViewBag.Categories = _categoryService.GetAll();
            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteTicket(int ticketId)
        {
            var entity = _ticketService.GetById(ticketId);
            if (entity != null)
            {
                _ticketService.Delete(entity);
            }
            return RedirectToAction("TicketList");
        }

        public IActionResult CategoryList()
        {
            return View(new CategoryListModel()
            {
                Categories = _categoryService.GetAll()
            });
        }
        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCategory(CategoryModel model)
        {
            var entity = new Category()
            {
                Name = model.Name
            };
            _categoryService.Create(entity);

            return RedirectToAction("CategoryList");
        }
        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            var entity = _categoryService.GetByIdWithTickets(id);

            return View(new CategoryModel()
            {
                Id = entity.Id,
                Name = entity.Name,
                Tickets = entity.TicketCategories.Select(p => p.Ticket).ToList()
            }); ;
        }
        [HttpPost]
        public IActionResult EditCategory(CategoryModel model)
        {
            var entity = _categoryService.GetById(model.Id);
            if (entity == null)
            {
                return NotFound();
            }

            entity.Name = model.Name;
            _categoryService.Update(entity);

            return RedirectToAction("CategoryList");
        }

        [HttpPost]
        public IActionResult DeleteCategory(int categoryId)
        {
            var entity = _categoryService.GetById(categoryId);

            if (entity != null)
            {
                _categoryService.Delete(entity);
            }

            return RedirectToAction("CategoryList");
        }

        [HttpPost]
        public IActionResult DeleteFromCategory(int categoryId, int ticketId)
        {
            _categoryService.DeleteFromCategory(categoryId, ticketId);
            return Redirect("/admin/editcategory/" + categoryId);
        }


    }
}