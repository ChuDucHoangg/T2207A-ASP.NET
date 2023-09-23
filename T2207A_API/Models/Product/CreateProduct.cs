using System;
using System.ComponentModel.DataAnnotations;
namespace T2207A_API.Models.Product
{
    public class CreateProduct
    {
        [Required(ErrorMessage = "Please enter a product name")]
        [MinLength(1, ErrorMessage = "Please enter a minimum of 6 characters")]
        [MaxLength(200, ErrorMessage = "Please enter a maxmum of 200 characters")]
        public string name { get; set; }

        [Required(ErrorMessage = "Please enter price")]
        public decimal price { get; set; }

        [Required(ErrorMessage = "Please enter Description")]
        public string description { get; set; }

        [Required(ErrorMessage = "Please choose image")]
        public string thumbnail { get; set; }

        [Required(ErrorMessage = "Please enter Qty")]
        public int qty { get; set; }

        [Required(ErrorMessage = "Plase enter Category")]
        public int category { get; set; }
    }
}

