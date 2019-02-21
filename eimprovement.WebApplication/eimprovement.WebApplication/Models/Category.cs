using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eimprovement.WebApplication.Models
{
    public class Category
    {

        public long id { get; set; }
        public string name { get; set; }

        public Category(long id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public Category(string name)
        {
          
            this.name = name;
        }
    }
}