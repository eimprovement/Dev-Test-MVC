using eimprovement.WebApplication.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eimprovement.WebApplication.Models
{
    public enum StatusEnum
    {
        [StringValue("Available")]
        Available = 1,
        [StringValue("Held")]
        Held = 2,
        [StringValue("Sold")]
        Sold = 3
    }
}