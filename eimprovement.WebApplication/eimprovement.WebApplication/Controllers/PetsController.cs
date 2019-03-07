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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(AddPetViewModel model)
        {
            if (ModelState.IsValid)
            {
                await Service.AddPetAsync(model);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        public async Task<ActionResult> Update(long id)
        {
            UpdatePetViewModel model = await Service.FindPetForUpdateAsync(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(UpdatePetViewModel model)
        {
            if (ModelState.IsValid)
            {
                await Service.UpdatePetAsync(model);
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(long id)
        {
            await Service.DeletePetAsync(id);
            return new EmptyResult();
        }
    }
}