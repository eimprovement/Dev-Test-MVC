using eimprovement.Domain.Models;
using eimprovement.Domain.Services;
using eimprovement.WebApplication.Helpers;
using eimprovement.WebApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace eimprovement.WebApplication.Controllers
{
    public class PetsController : Controller
    {
        // used for injection from Db source during development
        // IPetStoreData _data;

        // pull the key from the config file
        string baseUrl = "https://dev-test.azure-api.net/petstore/pet/";

        public PetsController(IPetStoreData data)
        {
            // this._data = data; // not using for API service
        }
        // GET: Pets
        public async Task<ActionResult> Index(string choice)
        {
            string status = "available";

            if (!string.IsNullOrEmpty(choice))
            {
                status = choice;
            }

            List<Pet> model = new List<Pet>();

            List<PetViewModel> models = new List<PetViewModel>();

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl + "pet");

                httpClient.DefaultRequestHeaders.Clear();
                //Define request data format  
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "4a974f8b78da4c2192f5fccc034b8686");
                HttpResponseMessage result = await httpClient.GetAsync("findByStatus?status=" + status);

                if (result.IsSuccessStatusCode)
                {
                    var petResponse = result.Content.ReadAsStringAsync().Result;
                    model = JsonConvert.DeserializeObject<List<Pet>>(petResponse);
                    string categoryName;
                    foreach (var mod in model)
                    {
                        models.Add(BuildViewModelFromEntity(mod));
                    }
                }

                return View(models);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Details(Int64 id)
        {
            Pet pet = new Pet();
            PetViewModel model;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                httpClient.DefaultRequestHeaders.Clear();
                //Define request data format  
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "4a974f8b78da4c2192f5fccc034b8686");
                var result = await httpClient.GetAsync(id.ToString()); 
                
                
                if (result.IsSuccessStatusCode)
                {
                    var readPet = result.Content.ReadAsAsync<Pet>();
                    readPet.Wait();

                    pet = readPet.Result;

                    // category not guaranteed
                    model = BuildViewModelFromEntity(pet);
                }
                else
                {
                    // log the error here
                    pet = null;
                    ModelState.AddModelError(string.Empty, "Server error");
                    return View("NotFound");
                }

                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PetEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                // build Pet domain model from view model
                Pet newPet = BuildPetEntity(model);

                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(baseUrl);

                    httpClient.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "4a974f8b78da4c2192f5fccc034b8686");
                    var response = await httpClient.PostAsJsonAsync<Pet>("", newPet);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["message"] = "You have successfully added this pet.";
                        return RedirectToAction("Details", new { id = newPet.id });
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server Error");
                        return View("Error");
                    }
                }
            }
            else
            {
                // go back to Create view
                return View();
            }

        }

        [HttpGet]
        public async Task<ActionResult> Edit(Int64 id)
        {
            Pet pet = new Pet();
            PetEditViewModel model;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                httpClient.DefaultRequestHeaders.Clear();
                //Define request data format  
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "4a974f8b78da4c2192f5fccc034b8686");
                var result = await httpClient.GetAsync(id.ToString());

                if (result.IsSuccessStatusCode)
                {
                    var readPet = result.Content.ReadAsAsync<Pet>();
                    readPet.Wait();

                    pet = readPet.Result;

                    model = BuildEditViewModelFromEntity(pet);
                }
                else
                {
                    // log the error here
                    pet = null;
                    ModelState.AddModelError(string.Empty, "Server error");
                    return View("NotFound");
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PetEditViewModel model)
        {

            if (ModelState.IsValid)
            {
                // build Pet domain model from view model
                Pet editPet = BuildPetEntity(model);

                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(baseUrl);

                    httpClient.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "4a974f8b78da4c2192f5fccc034b8686");

                    var response = await httpClient.PutAsJsonAsync("", editPet);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["message"] = "You have updated this pet.";
                        return RedirectToAction("Details", new { id = editPet.id });
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server Error");
                        return View();
                    }
                }
            }
            else
            {
                return View();
            }



        }

        [HttpGet]
        public async Task<ActionResult> Delete(Int64 id)
        {
            Pet pet = new Pet();
            PetViewModel model;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                httpClient.DefaultRequestHeaders.Clear();
                //Define request data format  
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "4a974f8b78da4c2192f5fccc034b8686");

                var result = await httpClient.GetAsync(id.ToString());

                if (result.IsSuccessStatusCode)
                {
                    var readPet = result.Content.ReadAsAsync<Pet>();
                    readPet.Wait();

                    pet = readPet.Result;
                    string categoryName = pet.category != null ? pet.category.name : "Not specified";
                    model = new PetViewModel
                    {
                        id = pet.id,
                        Name = pet.name,
                        Category = categoryName,
                        PhotoUrls = pet.photoUrls,
                        Status = pet.status
                    };

                }
                else
                {
                    // log the error here
                    pet = null;
                    ModelState.AddModelError(string.Empty, "Server error");
                    return View("NotFound");
                }
            }

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Pet model)
        {
            Pet pet = new Pet();
            var id = model.id;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                httpClient.DefaultRequestHeaders.Clear();
                //Define request data format  
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "4a974f8b78da4c2192f5fccc034b8686");
                var response = await httpClient.DeleteAsync(id.ToString());

                if (response.IsSuccessStatusCode)
                {
                    TempData["message"] = "You deleted " + id.ToString();
                    return RedirectToAction("Index");
                }
                else
                {
                    // log the error here
                    pet = null;
                    ModelState.AddModelError(string.Empty, "Server error");
                    return View("NotFound");
                }
            }
        }

        #region helper methods

        private static Pet BuildPetEntity(PetEditViewModel model)
        {
            return new Pet
            {
                id = model.id,
                category = new Category
                {
                    id = (Int64)model.category,
                    name = StringEnum.GetStringFromEnum(model.category)
                },
                name = model.name,
                photoUrls = new string[] { "cutedoggieurl" },
                tags = new[] { new Tag { id = 1, name = "cute" } },
                status = StringEnum.GetStringFromEnum(model.status)
            };
        }

        private static PetViewModel BuildViewModelFromEntity(Pet pet)
        {
            string categoryName = pet.category != null ? pet.category.name : "Not specified";
            return new PetViewModel
            {
                id = pet.id,
                Name = pet.name,
                Category = categoryName,
                PhotoUrls = pet.photoUrls,
                Status = pet.status
            };
        }

        private static PetEditViewModel BuildEditViewModelFromEntity(Pet pet)
        {
            PetEditViewModel model;
            StatusEnum valueAsEnumFromString;
            Enum.TryParse<StatusEnum>(pet.status, out valueAsEnumFromString);
            var values = Enum.GetValues(typeof(StatusEnum));

            // category not guaranteed
            long categoryId = pet.category != null ? pet.category.id : 0;
            model = new PetEditViewModel()
            {
                id = pet.id,
                category = (CategoryEnum)categoryId,
                name = pet.name,
                photoUrls = pet.photoUrls,
                status = valueAsEnumFromString
            };
            return model;
        }

        #endregion

    }
}