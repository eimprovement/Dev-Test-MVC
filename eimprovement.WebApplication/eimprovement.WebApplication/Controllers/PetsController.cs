using eimprovement.WebApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace eimprovement.WebApplication.Controllers
{
    public class PetsController : Controller
    {
        /// <summary>
        /// Static variable to manage the http requests
        /// </summary>
        private static HttpClient client = new HttpClient();

        /// <summary>
        /// Controller constructor
        /// </summary>
        public PetsController()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Clear();

            // Request headers
            client.DefaultRequestHeaders.Add("api_key", "");
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "ead0df22b7ca4787a56c0d5b717d4368");

            //Accept aplication/json
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Action to get the Available Pets from the Endpoint
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            String queryString = "";
            String uri = @"https://dev-test.azure-api.net/petstore/pet/findByStatus?status=available&" + queryString;
            List<Pet> pets = null;
            //ResponseHeadersRead returns as soon as the response headers can be read
            HttpResponseMessage response = await client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
            if (response.IsSuccessStatusCode)
            {
                pets = await response.Content.ReadAsAsync<List<Pet>>();
            }
            return View(pets.Take(1000));
        }

        /// <summary>
        /// Action to get the Available Pets from the Endpoint
        /// </summary>
        /// <returns></returns>
        /*[HttpGet]
        public async Task<ActionResult> GetAvailablePets()
        {
            String queryString = "";
            String uri = @"https://dev-test.azure-api.net/petstore/pet/findByStatus?status=available&" + queryString;
            List<Pet> pets = null;
            //ResponseHeadersRead returns as soon as the response headers can be read
            HttpResponseMessage response = await client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
            if (response.IsSuccessStatusCode)
            {
                pets = await response.Content.ReadAsAsync<List<Pet>>();
            }
            return View(pets.Take(1000));
        }*/

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Creates a new Pet using the EndPoint
        /// </summary>
        /// <param name="pet">The Pet to be created</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create(Pet pet)
        {
            String queryString = "";
            String uri = @"https://dev-test.azure-api.net/petstore/pet"+ "?" + queryString;

            var content = new StringContent(new JavaScriptSerializer().Serialize(pet), Encoding.UTF8, "application/json");
            
            var response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {//if the Pet was created, then redirect to the edit view of the new Pet
                pet = await response.Content.ReadAsAsync<Pet>();
                return RedirectToAction("Edit", new { id = pet.id });

            }
            return View(pet);
        }

        /// <summary>
        /// Action to Edit a Pet information
        /// </summary>
        /// <param name="id">The id of the Pet to modify</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Edit(long id)
        {
            String queryString = "";
            String uri = @"https://dev-test.azure-api.net/petstore/pet/" + id.ToString() + queryString;
            Pet pet = null;
            HttpResponseMessage response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {//The Pet was successfully recovered
                pet = await response.Content.ReadAsAsync<Pet>();
                return View(pet);
            }
            else
            {//Pet not found
                return new HttpNotFoundResult("Pet not Found...");
            }
        }

        /// <summary>
        /// Action that posts the Pet information and uses the EndPoint to update information
        /// </summary>
        /// <param name="pet">Contains the information from the Pet to update</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Edit(Pet pet)
        {
            String queryString = "";
            String uri = @"https://dev-test.azure-api.net/petstore/pet/" + pet.id + "?" + queryString;

            //Using FormUrlEncodedContent to store Pet information
            var content = new FormUrlEncodedContent(new[]
                        {
                new KeyValuePair<string, string>("id", pet.id.ToString()),
                new KeyValuePair<string, string>("name", pet.name),
                new KeyValuePair<string, string>("status",pet.status)
            });
            var response = await client.PostAsync(uri, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            return RedirectToAction("Edit", new { id = pet.id });
        }

        /// <summary>
        /// Action to Detele a Pet using the EndPoint
        /// </summary>
        /// <param name="id">id of the Pet to delete</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<JsonResult> Delete(long id)
        {
            var uri = "https://dev-test.azure-api.net/petstore/pet/" + id + "?";

            var response = await client.DeleteAsync(uri);
            if (response.IsSuccessStatusCode)
            {//returns a message to the View
                return Json("Pet was deleted successfully", JsonRequestBehavior.AllowGet);

            }
            //returns an Error Message
            return Json("There was an error deleting the pet. " + response.Content.ReadAsStringAsync(), JsonRequestBehavior.AllowGet);
        }
    }
}