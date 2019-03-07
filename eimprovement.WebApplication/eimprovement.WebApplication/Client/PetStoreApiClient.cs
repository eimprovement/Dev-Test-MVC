using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Newtonsoft.Json;

using eimprovement.WebApplication.Client.Models;
using System.Text;
using Newtonsoft.Json.Serialization;

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
                throw new PetStoreApiException($"Failed to retrieve pets with status available from petstore api. Api returned Status: {response.StatusCode}");
            }

            return await ReadContentAsAsync<List<PetResource>>(response);
        }

        public async Task AddPetAsync(PetResource petResource)
        {
            var url = "/petstore/pet";
            var json = SerializeToJson(petResource);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await Client.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                throw new PetStoreApiException($"Failed to add new pet to petstore api. Api returned Status: {response.StatusCode}");
            }
        }

        public async Task DeletePetAsync(long petId)
        {
            var url = $"petstore/pet/{petId}";
            HttpResponseMessage response = await Client.DeleteAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new PetStoreApiException($"Unable to delete pet from petstore api. Pet Id = {petId}. Api returned Status: {response.StatusCode}");
            }
        }

        private async Task<T> ReadContentAsAsync<T>(HttpResponseMessage response) {
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(content);
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