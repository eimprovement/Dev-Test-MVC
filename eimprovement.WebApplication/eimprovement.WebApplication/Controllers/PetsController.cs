using eimprovement.WebApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace eimprovement.WebApplication.Controllers
{
    public class PetsController : Controller
    {
        // GET: Pets
        public ActionResult Index()
        {
            try
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
            catch (Exception ex)
            {
                return View("Error");
            }
            
        }
    }
}