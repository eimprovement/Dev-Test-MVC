using eimprovement.WebApplication.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eimprovement.WebApplication.Models
{
    public enum CategoryEnum
    {
        [StringValue("None")]
        None = 0,
        [StringValue("canine")]
        Canine = 1,
        [StringValue("feline")]
        Feline = 2,
        [StringValue("reptile")]
        Reptile = 3
    }
}