using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eimprovement.WebApplication.Models
{
    public class PetCategory
    {

        public PetCategory(long id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public long id { get; set; }
        public string name { get; set; }
    }
}