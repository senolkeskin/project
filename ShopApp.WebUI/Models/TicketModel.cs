using ShopApp.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI.Models
{
    public class TicketModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(60,MinimumLength =1,ErrorMessage ="1 ila 60 karakter arasında karakter giriniz.")]
        public string To { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "1 ila 60 karakter arasında karakter giriniz.")]
        public string From { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        [StringLength(1200, MinimumLength = 1, ErrorMessage = "1 ila 120 karakter arasında karakter giriniz.")]
        public string Description { get; set; }
        [Required(ErrorMessage ="Fiyat giriniz.")]
        [Range(1,10000)]
        public decimal? Price { get; set; }
        public List<Category> SelectedCategories { get; set; }
    }
}
