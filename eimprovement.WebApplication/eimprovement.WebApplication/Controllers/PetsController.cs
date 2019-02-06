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
        private SCL.Pets Pets { get; set; }

        public PetsController()
        {
            Pets = new SCL.Pets();
        }

        // GET: Pets
        // Note: Improve the picture, and tags, and the complete UI
        public ActionResult Index()
        {
            try
            {
                var availablePets = Pets.GetAvailablePets().Take(2);
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

        //Get: Set as Sold
        //Notes: Improve UX, add a message
        [HttpGet]
        public ActionResult Sold(long id)
        {
            try
            {
                if (Pets.SetFlagsPetAsSold(id))
                {
                    return RedirectToAction("Index");
                } else
                {
                    throw new Exception("Problem deleting a pet");
                }
            }
            catch (Exception ex)
            {

                return View("Error");
            }            
        }



        // GET: PetsTest/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PetsTest/Create
        // Note, important, add a combo with categories, and a tags field
        // This controller does not work, The API connection does, refers to the Tests projetc
        [HttpPost]
        public ActionResult Create(FormCollection _pet)
        {
            try
            {
                // TODO: Validate Model
                var pet = new Models.Pet
                {
                    name = _pet["Name"],
                    category = new Models.Category { id = 0, name = _pet["Category"] },
                    photoUrls = new List<string> { _pet["photoUrls"] }
                };

                Pets.AddNewPet(pet);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}