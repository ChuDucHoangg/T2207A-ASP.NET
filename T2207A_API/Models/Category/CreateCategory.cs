using System;
using System.ComponentModel.DataAnnotations;
namespace T2207A_API.Models.Category
{
	public class CreateCategory
	{
        [Required(ErrorMessage = "Please enter a category name")]
        [MinLength(6, ErrorMessage = "Please enter a minimum of 6 characters")]
        [MaxLength(200, ErrorMessage = "Please enter a maxmum of 200 characters")] 
        public string name { get; set; }
	}
}

