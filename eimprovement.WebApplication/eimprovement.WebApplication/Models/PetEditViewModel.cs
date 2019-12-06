using System;
using System.ComponentModel.DataAnnotations;

namespace eimprovement.WebApplication.Models
{
    public class PetEditViewModel
    {
        [Required]
        [Display(Name = "Pet ID")]
        public Int64 id { get; set; }
        [Required]
        [Range(1, 3, ErrorMessage = "Please select a valid catetory")]
        [Display(Name = "Category")]
        public CategoryEnum category { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string name { get; set; }
        public string[] photoUrls { get; set; }
        [Required]
        [Range(1, 3, ErrorMessage = "Please select a valid status")]
        [Display(Name = "Status")]
        public StatusEnum status { get; set; }
    }
}