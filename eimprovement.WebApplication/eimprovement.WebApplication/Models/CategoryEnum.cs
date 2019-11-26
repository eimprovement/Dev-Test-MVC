using eimprovement.WebApplication.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eimprovement.WebApplication.Models
{
    public enum CategoryEnum
    {
        [StringValue("Canine")]
        Canine = 1,
        [StringValue("Feline")]
        Feline = 2,
        [StringValue("Reptile")]
        Reptile = 3
    }
}