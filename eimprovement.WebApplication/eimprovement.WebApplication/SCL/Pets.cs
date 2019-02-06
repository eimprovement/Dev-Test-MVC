using System;
using System.Collections.Generic;
using System.Configuration;
using eimprovement.WebApplication.Models;
using RestSharp;


namespace eimprovement.WebApplication.SCL
{
    public class Pets
    {
        private string ApiAccessPoint { get; set; }
        private string ApiSubscriptionKey { get; set; }

        public Pets()
        {
            ApiAccessPoint = ConfigurationManager.AppSettings.Get("ApiEndPoint");
            ApiSubscriptionKey = ConfigurationManager.AppSettings.Get("ApiSubscription");
        }

        /// <summary>
        /// Gets the available pets.
        /// </summary>
        /// <returns></returns>
        public List<Pet> GetAvailablePets()
        {
            try
            {
                RestClient client = new RestClient(ApiAccessPoint);
                var request = new RestRequest("petstore/pet/findByStatus?status=available");
                request.AddHeader("Ocp-Apim-Subscription-Key", ApiSubscriptionKey);

                var response = client.Get<List<Pet>>(request);
                return response.Data;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Add a exception log with the detail: {ex.Message}");
                return new List<Pet>();
            }
        }


        /// <summary>
        /// Flagses the pet as sold.
        /// Hi! I am not sure if to get the update data of the Pet or bring it again. I am going to use the
        /// option 2
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public bool SetFlagsPetAsSold(long id) {

            try
            {
                RestClient client = new RestClient(ApiAccessPoint);
                Pet pet = GetPetById(id);
                pet.status = "sold";

                RestRequest request = new RestRequest("petstore/pet");
                request.AddHeader("Ocp-Apim-Subscription-Key", ApiSubscriptionKey);
                pet.status = "sold";
                request.AddXmlBody(pet);

                var response = client.Put(request);
                return response.StatusCode == System.Net.HttpStatusCode.OK;

            }

            catch (Exception ex)
            {
                Console.WriteLine($"Add a exception log with the detail: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Ideally private
        /// </summary>
        /// <param name="id"></param>
        public Pet GetPetById(long id)
        {
            try
            {
                RestClient client = new RestClient(ApiAccessPoint);
                RestRequest request = new RestRequest($"petstore/pet/{ id }");
                request.AddHeader("Ocp-Apim-Subscription-Key", ApiSubscriptionKey);

                return client.Get<Pet>(request).Data;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Add a exception log with the detail: {ex.Message}");
                return new Pet();
            }            
        }


        /// <summary>
        /// Add new pet
        /// </summary>
        /// <param name="id"></param>
        public long? AddNewPet(Pet pet)
        {
            try
            {
                RestClient client = new RestClient(ApiAccessPoint);
                RestRequest request = new RestRequest($"petstore/pet/");
                request.AddHeader("Ocp-Apim-Subscription-Key", ApiSubscriptionKey);
                request.AddXmlBody(pet);

                var response = client.Post<Pet>(request).Data;
                return response.id;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Add a exception log with the detail: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Deletion of pet
        /// </summary>
        /// <param name="id"></param>
        public bool SoldPet(long id)
        {
            try
            {
                RestClient client = new RestClient(ApiAccessPoint);
                RestRequest request = new RestRequest($"petstore/pet/{id}");
                request.AddHeader("Ocp-Apim-Subscription-Key", ApiSubscriptionKey);
        
                var response = client.Delete(request);
                return response.StatusCode ==  System.Net.HttpStatusCode.OK;

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Add a exception log with the detail: {ex.Message}");
                return false;
            }
        }
    }
}