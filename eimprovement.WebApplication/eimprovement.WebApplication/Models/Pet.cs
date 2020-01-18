using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eimprovement.WebApplication.Models
{
    public class Pet
    {
        /// <summary>
        /// Constructor receiving a JToken
        /// </summary>
        /// <param name="jsonPet">JToken containing all the attributes of the class</param>
        public Pet(JToken jsonPet)
        {
            this.id = (long)jsonPet["id"];
            this.category = jsonPet["category"]?.ToObject<PetCategory>();
            this.name = (string)jsonPet["name"];
            this.photoUsrls = jsonPet["photoUrls"]?.ToList();
            this.status = (string)jsonPet["status"];
            this.tags = jsonPet["tags"]?.ToList();
            /*if (this.name != null && this.name.Length > 25)
            {
                this. name = this.name.Substring(0,25);
            }*/
        }

        /// <summary>
        /// Constructor receiving a string to be parsed as a JObject
        /// </summary>
        /// <param name="jsonString">String JSON containing all the attributes of the class</param>
        public Pet(string jsonString)
        {
            var jsonObject = JObject.Parse(jsonString);

            this.id = (long)jsonObject["id"];
            this.category = jsonObject["category"]?.ToObject<PetCategory>();
            this.name = (string)jsonObject["name"];
            this.photoUsrls = jsonObject["photoUrls"]?.ToList();
            this.status = (string)jsonObject["status"];
            this.tags = jsonObject["tags"]?.ToList();
        }

        /// <summary>
        /// Constructor of a new Pet with the 3 basic attributes
        /// </summary>
        /// <param name="id">Unique id for a pet</param>
        /// <param name="name">String name of the new pet.</param>
        /// <param name="status">String status of the new pet (active,pending,sold).</param>
        public Pet(long id, string name, string status)
        {
            this.id = id;
            this.name = name;
            this.status = status;
        }

        /// <summary>
        /// Method to return the JSON string of the class with the 3 main attributes (id, name, status)
        /// </summary>
        /// <returns>string JSON</returns>
        public string ToJsonString()
        {
            return "{\"id\":" + id + ", \"name\":\"" + name + "\", \"status\":\"" + status + "\"}";
        }

        public long id { get; set; }
        public PetCategory category { get; set; }
        public string name { get; set; }
        public List<JToken> photoUsrls { get; set; }
        public string status { get; set; }
        public List<JToken> tags { get; set; }
    }
}