using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eimprovement.WebApplication.Models
{
    public class Pet
    {
        public long id { get; set; }

        public Category category { get; set; }

        [Required]
        public string name { get; set; }

        public string[] photoUrls { get; set; }

        public List<Tag> tags { get; set; }

        public string status { get; set; }
    }
}