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
        available = 1,
        [StringValue("held")]
        held = 2,
        [StringValue("sold")]
        sold = 3
    }
}