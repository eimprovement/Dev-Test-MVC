using System;
using System.ComponentModel.DataAnnotations;

namespace eimprovement.WebApplication.Models
{
    public class PetViewModel
    {
        [Display(Name = "Pet ID")]
        public Int64 id { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string[] PhotoUrls { get; set; }
        public string[] Tags { get; set; }
        public string Status { get; set; }
    }
}