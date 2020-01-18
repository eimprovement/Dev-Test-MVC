using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eimprovement.WebApplication.Models
{
    public class PetList
    {
        /// <summary>
        /// Method that converts a JSON array string in a Pet List.
        /// </summary>
        /// <param name="json">JSON array string</param>
        /// <returns>List of pets parsed from a string</returns>
        public static List<Pet> createPetsList(string json)
        {
            var jsonArray = JArray.Parse(json);
            List<Pet> petsList = new List<Pet>();
            foreach (var jsonPet in jsonArray)
            {
                petsList.Add(new Pet(jsonPet));
            }
            return petsList;
        }
    }
}