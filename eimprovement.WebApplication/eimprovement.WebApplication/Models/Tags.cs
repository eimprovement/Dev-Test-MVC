using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eimprovement.WebApplication.Models
{
    public class Tags
    {
        private long id { get; set; }
        private string name { get; set; }

        public Tags(long id, string name)
        {

            this.id = id;
            this.name = name;
        }

        public Tags(string name)
        {

        
            this.name = name;
        }
    }
}