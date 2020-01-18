using eimprovement.WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace eimprovement.WebApplication.Controllers
{
    public class PetsController : Controller
    {
        // GET: Pets
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// This method get a pet based on the id, and then returns the EditPetModal to edit that pet.
        /// </summary>
        /// <param name="id">Unique id for a pet</param>
        /// <returns>EditPetModal view.</returns>
        [HttpGet]
        public ActionResult EditPetModal(ulong id)
        {
            Pet pet = APIRequests.getPetById(id);
            ViewBag.pet = pet;
            return PartialView();
        }

        /// <summary>
        /// This method returns the AddPetModal view.
        /// </summary>
        /// <returns>AddPetModal view.</returns>
        [HttpGet]
        public ActionResult AddPetModal()
        {
            return PartialView();
        }
        
        /// <summary>
        /// This method get all the pets from the system and pass them to the view. Then returns the view.
        /// </summary>
        /// <returns>PetList view</returns>
        [HttpGet]
        public ActionResult PetList()
        {
            List<Pet> soldPets = APIRequests.getPetsByStatus("sold");
            ViewBag.soldPets = soldPets;
            List<Pet> availablePets = APIRequests.getPetsByStatus("available");
            ViewBag.availablePets = availablePets;
            List<Pet> pendingPets = APIRequests.getPetsByStatus("pending");
            ViewBag.pendingPets = pendingPets;
            return View();
        }


        /// <summary>
        /// This method save changes for an existing pet based on the id.
        /// </summary>
        /// <param name="id">Unique id for a pet</param>
        /// <param name="name">String name of the new pet.</param>
        /// <param name="status">String status of the new pet (active,pending,sold).</param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult SavePet(long id, string name, string status)
        {
            var response = APIRequests.updatePet(id, name, status);
            return Json(new { data = response }, JsonRequestBehavior.AllowGet);
        }

        
        /// <summary>
        /// This method Add a new pet to the system.
        /// </summary>
        /// <param name="name">String name of the new pet.</param>
        /// <param name="status">String status of the new pet (active,pending,sold).</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddPet(string name, string status)
        {
            var response = APIRequests.addPet(name, status);
            return Json(new { data = response }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// This method deletes a Pet based on the id.
        /// </summary>
        /// <param name="id"> Unique Id for a pet</param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult DeletePet(double id)
        {
            var response = APIRequests.deletePet(id);
            return Json(new { data = response }, JsonRequestBehavior.AllowGet);
        }

    }
}