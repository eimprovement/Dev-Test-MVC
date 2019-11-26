using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eimprovement.WebApplication.Models
{
    public class PetCreateViewModel
    {
        public Int64 id { get; set; }
        public CategoryEnum category { get; set; }
        [Required]
        public string name { get; set; }
        public string[] photoUrls { get; set; }
        public StatusEnum status { get; set; }
    }
}