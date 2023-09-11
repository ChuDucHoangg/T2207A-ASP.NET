using System;
using System.ComponentModel.DataAnnotations;

namespace T2207A_MVC.Models
{
    public class ProductViewModel
    {
        [Required(ErrorMessage = "Please enter a category name")]
        [MinLength(6, ErrorMessage = "Please enter a minimum of 6 characters")]
        [Display(Name = "Name")]
        public string name { get; set; }
        public double price { get; set; }
        public string description { get; set; }
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
        [Display(Name = "Category")]
        public int category_id { get; set; }
        [Display(Name = "Brand")]
        public int brand_id { get; set; }
    }
}
