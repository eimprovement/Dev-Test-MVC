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

        public static implicit operator Category(string v)
        {
            throw new NotImplementedException();
        }
    }
}