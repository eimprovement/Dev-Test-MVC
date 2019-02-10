using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eimprovement.WebApplication.Models
{
    public class ApiResponse
    {
        #region Properties
        public int Code { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string Content { get; set; }
        #endregion
    }
}