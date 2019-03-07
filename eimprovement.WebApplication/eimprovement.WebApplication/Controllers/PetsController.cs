using System.Threading.Tasks;
using System.Web.Mvc;

using eimprovement.WebApplication.Models;
using eimprovement.WebApplication.Services;

namespace eimprovement.WebApplication.Controllers
{
    public class PetsController : Controller
    {
        public PetsController(IPetStoreService petStoreService) {
            Service = petStoreService;
        }

        public IPetStoreService Service { get; }

        public async Task<ActionResult> Index()
        {
            var pets = await Service.GetPetsAsync();
            return View(pets);
        }

        public async Task<ActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(AddPetViewModel model)
        {
            if (ModelState.IsValid)
            {
                await Service.AddPetAsync(model);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(long id)
        {
            await Service.DeletePet(id);
            return new EmptyResult();
        }
    }
}