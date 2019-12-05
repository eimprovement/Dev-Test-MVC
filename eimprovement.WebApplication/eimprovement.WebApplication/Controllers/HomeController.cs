using eimprovement.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eimprovement.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        IPetStoreData _data;

        public HomeController(IPetStoreData db)
        {
            _data = db;
        }

        public ActionResult Index()
        {
            var model = _data.GetAll();
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Filter(FormCollection formCollection)
        {
            string choice;

            return RedirectToAction("Index", "Pets", (new { choice = formCollection["name"] }));
        }
    }
}