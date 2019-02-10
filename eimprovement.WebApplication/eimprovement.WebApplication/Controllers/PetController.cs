using eimprovement.WebApplication.Models;
using eimprovement.WebApplication.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static eimprovement.WebApplication.Utils.ApiMethod;

namespace eimprovement.WebApplication.Controllers
{
    public class PetController : Controller
    {
        /// <summary>
        /// Gets a list of pets according indicated status. *For example purposes the list was cut off at 100 records and order by id*
        /// </summary>
        /// <param name="status">Status of the pet. *See Utils -> Enums for more info.*</param>
        /// <param name="renderView">Indicates wheter the full index view has to be rendered or just the partial view with the table for the records.</param>
        /// <returns></returns>        
        [HttpGet]
        public async Task<ActionResult> Index(string status = "available", bool renderView = true)
        {
            try
            {
                //Build request element
                Request request = new Request()
                {
                    //Specific request/endpoint url segment.
                    ActionUrl = "pet/findByStatus?status=",
                    //Indicate params if needed.
                    Params = HttpUtility.ParseQueryString(status).ToString(),
                    //Http attribute according to the api requirement.
                    Type = HttpAttributes.Get
                };

                //Send the request and store the result in ApiResponse model element.
                ApiResponse apiResponseModel = await SendGetDeleteRequest(request);

                //Validate request response status code.
                if (apiResponseModel.Code == (int)HttpStatusCode.OK)
                {
                    List<Pet> petList = JsonConvert.DeserializeObject<List<Pet>>(apiResponseModel.Content).OrderByDescending(p => p.Id).Take(100).ToList();

                    //Only render partial view with table and results.
                    if (!renderView)
                    {
                        return PartialView("_Table", petList);                       
                    }

                    //Render full index view.
                    return View(petList);
                }

                return View("~/Views/Shared/Error.cshtml");
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Instantiate a new pet model for create view.
        /// </summary>
        /// <returns>Create view.</returns>
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Pet());
        }

        /// <summary>
        /// Creates a new pet.
        /// </summary>
        /// <param name="pet">Pet object to be created.</param>
        /// <returns>Index view in case creation was ok or error view for any other states.</returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Pet pet)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Request request = new Request()
                    {
                        ActionUrl = "pet/",
                        RequestBody = JsonConvert.SerializeObject(pet),
                        Type = HttpAttributes.Post
                    };

                    ApiResponse apiResponseModel = await SendPostPutRequest(request);

                    if (apiResponseModel.Code == (int)HttpStatusCode.OK)
                    {
                        return RedirectToAction("Index");
                    } 
                }

                return View(pet);
                
            }
            catch (Exception)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        /// <summary>
        /// Gets pet info by given id to load data in edition mode.
        /// </summary>
        /// <param name="id">Pet id.</param>
        /// <returns>Edit view with information loaded in case requests was ok or error view for any other state.</returns>
        [HttpGet]
        public async Task<ActionResult> Edit(long id)
        {
            try
            {
                Request request = new Request()
                {
                    ActionUrl = "pet/",
                    Params = id.ToString(),
                    Type = HttpAttributes.Get
                };

                ApiResponse apiResponseModel = await SendGetDeleteRequest(request);

                if (apiResponseModel.Code == (int)HttpStatusCode.OK)
                {
                    Pet pet = JsonConvert.DeserializeObject<Pet>(apiResponseModel.Content);
                    return View(pet); 
                }

                return View("~/Views/Shared/Error.cshtml");
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Updates pet info.
        /// </summary>
        /// <param name="pet">Pet object to be updated.</param>
        /// <returns>Index view in case creation was ok or error view for any other states.</returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(Pet pet)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Request request = new Request()
                    {
                        ActionUrl = "pet/",
                        RequestBody = JsonConvert.SerializeObject(pet),
                        Type = HttpAttributes.Put
                    };

                    ApiResponse apiResponseModel = await SendPostPutRequest(request);

                    if (apiResponseModel.Code == (int)HttpStatusCode.OK)
                    {
                        return RedirectToAction("Index", new { status = "available", initial = true });
                    } 
                }

                return View(pet);
            }
            catch (Exception)
            {
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        /// <summary>
        /// Deletes a pet.
        /// </summary>
        /// <param name="id">Pet id.</param>
        /// <returns>Error message in case request failed or empty result if it was successful. Javascript reloads index view.</returns>
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(long id)
        {
            try
            {
                Request request = new Request()
                {
                    ActionUrl = "pet/",
                    Params = id.ToString(),
                    Type = HttpAttributes.Delete
                };

                ApiResponse apiResponseModel = await SendGetDeleteRequest(request);

                if (apiResponseModel.Code != (int)HttpStatusCode.OK)
                {
                    return View("~/Views/Shared/Error.cshtml");
                }

                return new EmptyResult();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}