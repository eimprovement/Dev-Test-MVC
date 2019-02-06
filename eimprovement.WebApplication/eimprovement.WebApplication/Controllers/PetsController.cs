using eimprovement.WebApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
                var result = new List<PetViewModel>();
                var availablePets = Pets.GetAvailablePets();

                if (availablePets.Any()) { 
                    result = availablePets.Select(x => new PetViewModel
                        {
                            Id = x.id,
                            Category = x.category != null ? x.category.name : string.Empty,
                            Name = x.name                           
                        }).ToList();
                    }
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
                Pets.AddNewPet(new Models.Pet
                {
                    category = new Models.Category
                    {
                        id = 0,
                        name = _pet["Category"]
                    },
                    name = _pet["Name"],
                    photoUrls = new List<string> {_pet["PhotoUrl"]},
                    status = "available",
                    tags = new List<Models.Tag> {
                         new Models.Tag
                         {
                             id=0,
                             name = _pet["Tag"]
                         }
                 }
                });

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        //Get: Set as Sold
        //Notes: Improve UX, add a message
        [HttpGet]
        public ActionResult Remove(long id)
        {
            try
            {
                if (Pets.RemovePet(id))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    throw new Exception("Problem deleting a pet");
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

    }
}