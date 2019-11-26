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
        string baseUrl = "https://dev-test.azure-api.net/petstore/pet/";

        public PetsController(IPetStoreData data)
        {
            // this._data = data; // not using for API service
        }
        // GET: Pets
        public async Task<ActionResult> Index()
        {

            // not the preferred method for using HttpClient 
            // which are designed for long life. Would move into a 
            // Service project and continue the IoC pattern 

            List<Pet> model = new List<Pet>();

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl + "pet");

                httpClient.DefaultRequestHeaders.Clear();
                //Define request data format  
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "4a974f8b78da4c2192f5fccc034b8686");
                HttpResponseMessage result = await httpClient.GetAsync("findByStatus?status=available");

                if (result.IsSuccessStatusCode)
                {
                    var petResponse = result.Content.ReadAsStringAsync().Result;
                    model = JsonConvert.DeserializeObject<List<Pet>>(petResponse);
                }

                return View(model);
            }

            // mockup data used while building out the client scaffolding
            // var model = _data.FindByStatus("available");
            //return View(model);



        }

        public ActionResult Details(Int64 id)
        {
            Pet pet = new Pet();

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                httpClient.DefaultRequestHeaders.Clear();
                //Define request data format  
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "4a974f8b78da4c2192f5fccc034b8686");
                var getTask = httpClient.GetAsync(id.ToString());

                getTask.Wait();

                var result = getTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readPet = result.Content.ReadAsAsync<Pet>();
                    readPet.Wait();

                    pet = readPet.Result;
                }
                else
                {
                    // log the error here
                    pet = null;
                    ModelState.AddModelError(string.Empty, "Server error");
                    return View("NotFound");
                }

                return View(pet);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PetCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                // build Pet domain model from view model
                Pet newPet = new Pet
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

                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(baseUrl);

                    httpClient.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "4a974f8b78da4c2192f5fccc034b8686");
                    var postTask = httpClient.PostAsJsonAsync<Pet>("", newPet);
                    postTask.Wait();
                    var response = postTask.Result;

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server Error");
                    }

                }
                return RedirectToAction("Details", new { id = newPet.id });
            }
            else
            {
                // go back to Create view
                return View();
            }

        }

        [HttpGet]
        public ActionResult Edit(Int64 id)
        {

            Pet pet = new Pet();

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                httpClient.DefaultRequestHeaders.Clear();
                //Define request data format  
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "4a974f8b78da4c2192f5fccc034b8686");
                var getTask = httpClient.GetAsync(id.ToString());
                getTask.Wait();

                var result = getTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readPet = result.Content.ReadAsAsync<Pet>();
                    readPet.Wait();

                    pet = readPet.Result;
                }
                else
                {
                    // log the error here
                    pet = null;
                    ModelState.AddModelError(string.Empty, "Server error");
                    return View("NotFound");
                }
            }

            return View(pet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pet model)
        {

            if (ModelState.IsValid)
            {
                // build Pet domain model from view model
                Pet editPet = new Pet
                {
                    id = model.id,
                    name = model.name,
                    category = new Category
                    {
                        id = 1,
                        name = "Canine"
                    },
                    //Category = new Category { id = model.Category.id, name = model.Category.name },
                    photoUrls = new string[] { "cutedoggieurl" },
                    status = model.status,
                    tags = new[] { new Tag { id = 1, name = "cute" } }
                };

                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri(baseUrl);

                    httpClient.DefaultRequestHeaders.Clear();
                    //Define request data format  
                    httpClient.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "4a974f8b78da4c2192f5fccc034b8686");

                    var putTask = httpClient.PutAsJsonAsync("", editPet);
                    putTask.Wait();
                    var response = putTask.Result;

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Details", new { id = editPet.id });
                        //return View("Details", editPet);
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
        public ActionResult Delete(Int64 id)
        {
            Pet pet = new Pet();

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(baseUrl);
                httpClient.DefaultRequestHeaders.Clear();
                //Define request data format  
                httpClient.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "4a974f8b78da4c2192f5fccc034b8686");
                var getTask = httpClient.GetAsync(id.ToString());
                getTask.Wait();

                var result = getTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readPet = result.Content.ReadAsAsync<Pet>();
                    readPet.Wait();

                    pet = readPet.Result;
                }
                else
                {
                    // log the error here
                    pet = null;
                    ModelState.AddModelError(string.Empty, "Server error");
                    return View("NotFound");
                }
            }

            return View(pet);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Pet model)
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
                var deleteTask = httpClient.DeleteAsync(id.ToString());

                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
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


    }
}