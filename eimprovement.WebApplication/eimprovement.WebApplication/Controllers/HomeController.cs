using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http.Headers;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using eimprovement.WebApplication.Models;
using Newtonsoft.Json.Linq;

namespace eimprovement.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        //SubscriptionKey of my user
        string SubscriptionKey = "de17c5de72b44de48af91e837005e6a2";

        //Here we will take all the values from the JSON field
        public async Task<ActionResult> Index()
        {
            //Initialize HTTP values
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            //Send the header to the API
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);

            //API address
            var uri = "https://dev-test.azure-api.net/petstore/pet/findByStatus?status=available";

            //Retrieve the JSON
            var response = await client.GetAsync(uri);
            var resultM = response.Content.ReadAsStringAsync().Result;
            var result = response.Content.ReadAsStringAsync().Result;
            var s = Newtonsoft.Json.JsonConvert.DeserializeObject(result);
            JArray json = JArray.Parse(s.ToString());


            //Create List for storage the JSON info
            List<Pets> pets = new List<Pets>();
            
            //Load the data according of what is on the JSON
            foreach (JObject item in json)
            {
                var id = item.GetValue("id");
                var name = item.GetValue("name");
                var category = item.GetValue("category");
                var photoURLs = item.GetValue("photoUrls");
                var tagsID = item.SelectToken("tags[0].id");
                var tagsNormal = item.GetValue("tags");
                var tagsName = item.SelectToken("tags[0].name");
                var status = item.GetValue("status");
                var categoryID = item.SelectToken("category.id");
                var categoryName = item.SelectToken("category.name");

                //Some tags fields are empty, so we avoid an error in here
                if (tagsID == null || tagsNormal == null || tagsName == null)
                {

                    tagsID = "";
                    tagsName = "";
                    tagsNormal = "";


                }



                try
                {
                    //Storage the data in Pet List
                    pets.Add(new Pets(Int64.Parse(id.ToString()),
                        name.ToString(),
                        new Category(Int64.Parse(categoryID.ToString()), categoryName.ToString()),
                        GetPhotoURL(photoURLs.ToString(), Int32.Parse(photoURLs.Count().ToString())),
                        status.ToString(),
                        GetTags(Int64.Parse(tagsID.ToString()), tagsName.ToString(), tagsNormal.Count())));




                }
                catch (Exception ex)
                {
                    Console.WriteLine("stop");

                }

            }
            //Call the model
            Pets pet = new Pets(pets);

            //return the model
            return View(pet);
        }
        [HttpPost]
        //Here you will send the id with the model to the next view
        //in order to add, update and delete according to the user id selection
        public async Task<ActionResult> FillDataAsync(string submitbutton, Pets modelPets)
        {

            switch (submitbutton)
            {
                case "Delete item":

                    return (await DeleteDataAsync(modelPets));

                case "Retrieve Data":
                    //In

                    //Initialize HTTP values
                    var client = new HttpClient();
                    var queryString = HttpUtility.ParseQueryString(string.Empty);

                    //Send the header to the API
                    client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);

                    //API address
                    var uri = "https://dev-test.azure-api.net/petstore/pet/" + modelPets.id;

                    //Retrieve the JSON
                    var response = await client.GetAsync(uri);
                    var resultM = response.Content.ReadAsStringAsync().Result;
                    var result = response.Content.ReadAsStringAsync().Result;
                    var s = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(result);

                    //We create a list with the data of the id selected
                    List<Pets> listtoSend = new List<Pets>();

                    //Create List for storage the JSON info
                    List<Pets> pets = new List<Pets>();
                    List<PetList> petL = new List<PetList>();


                    var id = s.id;
                    var name = s.name;
                    var category = s.category;
                    var photoURLs = s.photoUrls;
                    var status = s.status;
                    var tagsNormal = s.tags;
                    string idS = id;
                    string nameS = name;
                    string tags = tagsNormal.ToString();
                    string photo = photoURLs.ToString();
                    string categoryS = category.ToString();
                    string statusS = status.ToString();


                    //foreach (JObject item in json)
                    //    {
                    /*var id = item.GetValue("id");
                    var name = item.GetValue("name");
                    var category = item.GetValue("category");
                    var photoURLs = item.GetValue("photoUrls");
                    var tagsID = item.SelectToken("tags[0].id");
                    var tagsNormal = item.GetValue("tags");
                    var tagsName = item.SelectToken("tags[0].name");
                    var status = item.GetValue("status");
                    var categoryID = item.SelectToken("category.id");
                    var categoryName = item.SelectToken("category.name");

                    //Some tags fields are empty, so we avoid an error in here
                    if (tagsID == null || tagsNormal == null || tagsName == null)
                    {

                        tagsID = "";
                        tagsName = "";
                        tagsNormal = "";


                    }*/



                    try
                    {
                        //Storage the data in Pet List
                        //pets.Add(new Pets(Int64.Parse(id.ToString()),
                        //    name.ToString(),
                        //    new Category(category),
                        //    photoURLs,
                        //    status.ToString(),
                        //    tagsNormal));


                        petL.Add(new PetList(idS,
                                nameS,
                                categoryS,
                                photo,
                                status.ToString(),
                                tags));




                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("stop");

                    }

                    //}
                    PetList pet = new PetList(pets);

                    //return the model
                    return View("UpdatePet", pet);


                default:

                    return View("Index");


            }

            return View("Index");
        }


        //Get the GetTagsData
        public Tags[] GetTags(long id, string nameD, int size)
        {

            Tags[] tagsA = new Tags[size];

            Tags tagO;

            for (int i = 0; i < size; i++)
            {
                tagO = new Tags(id, nameD);

                tagsA[i] = tagO;


            }

            return tagsA;
        }

        //Get the photoURLData
        public string[] GetPhotoURL(string data, int size)
        {

            string[] photo = new String[size];

            for (int i = 0; i < size; i++)
            {

                photo[i] = data;

            }


            return photo;
        }

        public ActionResult addPage()

        {

            return View();
        }

        public async Task<ActionResult> DeleteDataAsync( Pets modelPets)
        {

            //itialize HTTP values
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            //Send the header to the API
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", SubscriptionKey);

            //API address
            var uri = "https://dev-test.azure-api.net/petstore/pet/" + modelPets.id;

            //Retrieve the JSON
            var response = await client.GetAsync(uri);
            
            try
            {
            

            }
            catch (Exception ex)
            {
                Console.WriteLine("stop");

            }

            //}


            //return the model
            return (await Index());
        }


    }
}