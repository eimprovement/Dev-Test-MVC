using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace eimprovement.WebApplication.Models
{
    public class APIRequests
    {
        /// <summary>
        /// Subscription key header name for http API requests
        /// </summary>
        public static string API_KEY_NAME = "Ocp-Apim-Subscription-Key";

        /// <summary>
        /// eImprovement API key
        /// </summary>
        public static string API_KEY = "e8f4f0aa3bc246179a3edb633166187c";

        /// <summary>
        /// This method get all the pets from the eImprovement API, based on the status.
        /// </summary>
        /// <param name="status">String status of the new pet (active,pending,sold).</param>
        /// <returns>A list of Pet objects filtered by the status.</returns>
        public static List<Pet> getPetsByStatus(string status)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add(API_KEY_NAME, API_KEY);

            var uri = "https://dev-test.azure-api.net/petstore/pet/findByStatus?status=" + status;
            var response = client.GetAsync(uri).Result;
            var jsonString = response.Content.ReadAsStringAsync().Result;

            return PetList.createPetsList(jsonString).OrderBy(x => x.id).Take(5).ToList();
        }

        /// <summary>
        /// This method get one pet from the eImprovement API, based on the unique id.
        /// </summary>
        /// <param name="id">Unique Id for a pet.</param>
        /// <returns>A Pet object based on the id.</returns>
        public static Pet getPetById(ulong id)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add(API_KEY_NAME, API_KEY);

            var uri = "https://dev-test.azure-api.net/petstore/pet/" + id;
            var response = client.GetAsync(uri).Result;
            var jsonString = response.Content.ReadAsStringAsync().Result;

            return new Pet(jsonString);
        }

        /// <summary>
        /// This method udate the pet properties in the eImprovement API based on the unique id.
        /// </summary>
        /// <param name="id"> Unique Id for a pet.</param>
        /// <param name="name">String name of the new pet.</param>
        /// <param name="status">String status of the new pet (active,pending,sold).</param>
        /// <returns></returns>
        public static string updatePet(long id, string name, string status)
        {
            var client = new HttpClient();
            var pet = new Pet(id, name, status);
            client.DefaultRequestHeaders.Add(API_KEY_NAME, API_KEY);

            var uri = "https://dev-test.azure-api.net/petstore/pet";
            byte[] byteData = Encoding.UTF8.GetBytes(pet.ToJsonString());
            var content = new ByteArrayContent(byteData);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = client.PutAsync(uri, content).Result;
            var jsonString = response.Content.ReadAsStringAsync().Result;

            return jsonString;
        }

        /// <summary>
        /// This method add a new Pet to the eImprovement API with the two main attributes (name and status).
        /// </summary>
        /// <param name="name">String name of the new pet.</param>
        /// <param name="status">String status of the new pet (active,pending,sold).</param>
        /// <returns></returns>
        public static string addPet(string name, string status)
        {
            var client = new HttpClient();
            var pet = new Pet(0, name, status);
            client.DefaultRequestHeaders.Add(API_KEY_NAME, API_KEY);

            var uri = "https://dev-test.azure-api.net/petstore/pet";
            byte[] byteData = Encoding.UTF8.GetBytes(pet.ToJsonString());
            var content = new ByteArrayContent(byteData);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = client.PutAsync(uri, content).Result;
            var jsonString = response.Content.ReadAsStringAsync().Result;

            return jsonString;
        }

        /// <summary>
        /// This method delete a pet based on the id
        /// </summary>
        /// <param name="id"> Unique Id for a pet.</param>
        /// <returns></returns>
        public static string deletePet(double id)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add(API_KEY_NAME, API_KEY);

            var uri = "https://dev-test.azure-api.net/petstore/pet/" + id;
            var response = client.DeleteAsync(uri).Result;
            var jsonString = response.Content.ReadAsStringAsync().Result;

            return jsonString;
        }
    }
}