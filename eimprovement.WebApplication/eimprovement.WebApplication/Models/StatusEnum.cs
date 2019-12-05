using eimprovement.WebApplication.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eimprovement.WebApplication.Models
{
    public enum StatusEnum
    {
        [StringValue("available")]
        Available = 1,
        [StringValue("held")]
        Held = 2,
        [StringValue("sold")]
        Sold = 3
    }
}