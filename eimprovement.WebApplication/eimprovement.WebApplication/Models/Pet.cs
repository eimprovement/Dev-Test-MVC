using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eimprovement.WebApplication.Models
{
    public class Pet
    {
        public long id { get; set; }
        public Category category { get; set; }
        [DisplayName("Pet Name")]
        public string name { get; set; }
        public IList<string> photoUrls { get; set; }
        public IList<Tag> tags { get; set; }
        [DisplayName("Pet Status")]
        public string status { get; set; }

        public static List<SelectListItem> statuses = new List<SelectListItem>
        {
            new SelectListItem { Value = "available", Text = "available" },
            new SelectListItem { Value = "sold", Text = "sold" }
        };
    }

    public class Category
    {
        public long id { get; set; }
        public string name { get; set; }
    }

    public class Tag
    {
        public long id { get; set; }
        public string name { get; set; }
    }
}