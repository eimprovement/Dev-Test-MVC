using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using eimprovement.WebApplication.Models;
using eimprovement.Proxy.Pet;
using System.Net;
using Newtonsoft.Json;
using PagedList;
using PagedList.Mvc;
using eimprovement.WebApplication.Enum;

namespace eimprovement.WebApplication.Controllers
{
    public class PetController : Controller
    {
        PetProxy callProxy = new PetProxy();

        [HttpGet]
        public async Task<ActionResult> Index(int? page, string statusPet = "available")
        {
            List<Pet> petList = new List<Pet>();
            var response = await callProxy.GetPets(statusPet);

            if (response.code == (int)HttpStatusCode.OK)
            {
                petList = JsonConvert.DeserializeObject<List<Pet>>(response.content).ToList();
            }

            return View(petList.OrderByDescending(x=> x.id).ToPagedList(page ?? 1, 6));
        }

        [HttpGet]
        public ActionResult Create()
        {
            var pet = new Pet();
            return View(pet);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Pet pet)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var body = JsonConvert.SerializeObject(pet);
                    var response = await callProxy.AddPet(body);    
                }
            }
            catch
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            Pet pet = new Pet();
            try
            {
                var response = await callProxy.GetPetById(id);
                if (response.code == (int)HttpStatusCode.OK)
                {
                    pet = JsonConvert.DeserializeObject<Pet>(response.content);
                    return View(pet);
                }
            }
            catch
            {
               return View("~/Views/Shared/Error.cshtml");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(Pet pet)
        {            
            try
            {
                var response = await callProxy.DeletePet(pet.id.ToString());
            }
            catch
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            Pet pet = new Pet();
            try
            {
                var response = await callProxy.GetPetById(id);
                if (response.code == (int)HttpStatusCode.OK)
                {
                    pet = JsonConvert.DeserializeObject<Pet>(response.content);
                    pet.status = "sold";
                    return View(pet);
                }
            }
            catch
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Pet pet)
        {
            try
            {
                var body = JsonConvert.SerializeObject(pet);
                var response = await callProxy.UpdatePet(body);
            }
            catch
            {
                return View("~/Views/Shared/Error.cshtml");
            }
            return RedirectToAction("Index");
        }
    }
}