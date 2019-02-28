using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eimprovement.WebApplication.Controllers
{
    public class BaseController : Controller
    {
        public BaseController()
        {
            ViewData["PetsApiBaseUrl"] = ConfigurationManager.AppSettings["PetsApiBaseUrl"];
            ViewData["PetsApiKey"] = ConfigurationManager.AppSettings["PetsApiKey"];
        }
    }
}