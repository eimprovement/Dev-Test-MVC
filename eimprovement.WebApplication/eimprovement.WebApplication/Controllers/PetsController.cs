using eimprovement.Domain.Models;
using eimprovement.Domain.Services;
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
    }
}