using eimprovement.WebApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace eimprovement.WebApplication.Controllers
{
    public class PetShopController : Controller
    {
       const string subscriptionKey = "special-key"; 
       const string uriBase = "https://petstore.swagger.io/v2/"; //"https://dev-test.azure-api.net/petstore/";

        // GET: Pet List By Status       
        public async Task<ActionResult> Index()
        {
            List<PetShop> petInfo = new List<PetShop>();

            using (var client = new HttpClient())
            {           
                //Passing service base url  
                client.BaseAddress = new Uri(uriBase);
                client.DefaultRequestHeaders.Add("api-key", subscriptionKey);
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource findByStatus using HttpClient  
                HttpResponseMessage ShopPetresource = await client.GetAsync("pet/findByStatus?status=available");

                //Checking the response is successful or not which is sent using HttpClient  
                if (ShopPetresource.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var petShopResponse = ShopPetresource.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the Pet list  
                    petInfo = JsonConvert.DeserializeObject<List<PetShop>>(petShopResponse);

                }
                //returning the Pet list to view  
                return View(petInfo);
            }
        }
   
        public async Task<ActionResult> Edit(PetShop petShop)
        {
            string petInfo = string.Empty;
            using (var client = new HttpClient())
            {
                var queryString = HttpUtility.ParseQueryString(string.Empty);

                //Passing service base url  
                client.BaseAddress = new Uri(uriBase);
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource findByStatus using HttpClient                
                HttpResponseMessage ShopPetresource = await client.PutAsJsonAsync("pet/{petId}", petShop);

                //Checking the response is successful or not which is sent using HttpClient   
                if (ShopPetresource.IsSuccessStatusCode)
                {
                    petInfo = ShopPetresource.Content.ReadAsStringAsync().Result;
                }
                return View(petInfo);
            }
        }

        public async Task<ActionResult> Create(PetShop petShop)
        {
            string petInfo = string.Empty;
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(uriBase);
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource findByStatus using HttpClient                
                HttpResponseMessage ShopPetresource = await client.PostAsJsonAsync("pet/{petId}", petShop);

                //Checking the response is successful or not which is sent using HttpClient  
                if (ShopPetresource.IsSuccessStatusCode)
                {
                    petInfo = ShopPetresource.Content.ReadAsStringAsync().Result;
                }
                return View(petInfo);
            }
        }

        public async Task<ActionResult> Delete(PetShop petShop)
        {
            string petInfo = string.Empty;
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(uriBase);
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource findByStatus using HttpClient                
                HttpResponseMessage ShopPetresource = await client.DeleteAsync("pet/{petId}");

                //Checking the response is successful or not which is sent using HttpClient  
                if (ShopPetresource.IsSuccessStatusCode)
                {
                    petInfo = ShopPetresource.Content.ReadAsStringAsync().Result;
                }
                return View(petInfo);
            }
        }
    }
}