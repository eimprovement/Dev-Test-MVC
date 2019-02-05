using eimprovement.WebApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eimprovement.WebApplication.Controllers
{
    public class PetsTestController : Controller
    {
        // GET: PetsTest
        public ActionResult Index()
        {
            var pets = new SCL.Pets();

            var availablePets = pets.GetAvailablePets().Take(2);     
            var result = availablePets.Select(x => new PetViewModel
            {

                Id = x.id,
                Caterory = x.category.name,
                Name = x.name,
                PhotoUrl = x.photoUrls.Any() ? x.photoUrls.First() : string.Empty,
                Tags = x.tags.Any() ? x.tags.Select(t => t.name).ToArray() : new string[0]
            });

         
            return View(result);
        }

        // GET: PetsTest/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PetsTest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PetsTest/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PetsTest/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PetsTest/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PetsTest/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PetsTest/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
