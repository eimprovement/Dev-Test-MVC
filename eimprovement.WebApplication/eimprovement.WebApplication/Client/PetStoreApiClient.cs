using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

using eimprovement.WebApplication.Client.Models;

namespace eimprovement.WebApplication.Client
{
    public class PetStoreApiClient : IPetStoreApiClient
    {
        private const string ApiKeyHeaderName = "Ocp-Apim-Subscription-Key";

        public PetStoreApiClient(Uri baseAdress, string apiKey)
        {
            Client = CreateClient(baseAdress, apiKey);
        }

        private HttpClient Client { get; }

        public async Task<IEnumerable<PetResource>> GetAvailablePetsAsync()
        {
            var url = "petstore/pet/findByStatus?status=available";
            HttpResponseMessage response = await Client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new PetStoreApiException(
                    $"Failed to retrieve pets with status available from petstore api. Api returned Status: {response.StatusCode}");
            }

            return await ReadContentAsAsync<List<PetResource>>(response);
        }

        public async Task<PetResource> FindPetByIdAsync(long petId)
        {
            var url = $"petstore/pet/{petId}";
            HttpResponseMessage response = await Client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new PetStoreApiException(
                    $"Failed to add find pet in petstore api. Pet Id = {petId}. Api returned Status: {response.StatusCode}");
            }

            return await ReadContentAsAsync<PetResource>(response);
        }

        public async Task AddPetAsync(PetResource pet)
        {
            var url = "/petstore/pet";
            HttpContent content = CreateJsonContent(pet);

            HttpResponseMessage response = await Client.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new PetStoreApiException(
                    $"Failed to add new pet to petstore api. Api returned Status: {response.StatusCode}");
            }
        }

        public async Task UpdatePetAsync(PetResource pet)
        {
            var url = @"petstore/pet";
            HttpContent content = CreateJsonContent(pet);

            HttpResponseMessage response = await Client.PutAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new PetStoreApiException(
                    $"Failed to update pet in petstore api. Pet Id = {pet.Id}. Api returned Status: {response.StatusCode}");
            }
        }

        public async Task DeletePetAsync(long petId)
        {
            var url = $"petstore/pet/{petId}";
            HttpResponseMessage response = await Client.DeleteAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new PetStoreApiException(
                    $"Unable to delete pet from petstore api. Pet Id = {petId}. Api returned Status: {response.StatusCode}");
            }
        }

        private async Task<T> ReadContentAsAsync<T>(HttpResponseMessage response) {
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
        }

        private HttpContent CreateJsonContent(object fromObject) {
            string json = SerializeToJson(fromObject);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        private string SerializeToJson(object toSerialize) {
            return JsonConvert.SerializeObject(
                toSerialize, 
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        private HttpClient CreateClient(Uri baseAdress, string apiKey)
        {
            var client = new HttpClient
            {
                BaseAddress = baseAdress
            };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add(ApiKeyHeaderName, apiKey);

            return client;
        }
    }
}