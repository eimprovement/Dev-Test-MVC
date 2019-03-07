using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using eimprovement.WebApplication.Client;
using eimprovement.WebApplication.Client.Models;
using eimprovement.WebApplication.Models;

namespace eimprovement.WebApplication.Services
{
    public class PetStoreService : IPetStoreService
    {
        public PetStoreService(IPetStoreApiClient apiClient)
        {
            Client = apiClient;
        }

        private IPetStoreApiClient Client { get; }

        public async Task<IEnumerable<PetViewModel>> GetPetsAsync()
        {
            IEnumerable<PetResource> availablePets = await Client.GetAvailablePetsAsync();
            return availablePets.Select(MapToPetViewModel);
        }

        public async Task AddPetAsync(AddPetViewModel model)
        {
            var newPet = PetResource.CreateNew(model.Name);

            await Client.AddPetAsync(newPet);
        }

        public async Task DeletePet(long petId)
        {
            await Client.DeletePetAsync(petId);
        }

        private PetViewModel MapToPetViewModel(PetResource petResource)
        {
            return new PetViewModel
            {
                Id = petResource.Id,
                Name = petResource.Name
            };
        }
    }
}