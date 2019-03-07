using System.Collections.Generic;
using System.Threading.Tasks;

using eimprovement.WebApplication.Client.Models;

namespace eimprovement.WebApplication.Client
{
    public interface IPetStoreApiClient
    {
        Task<IEnumerable<PetResource>> GetAvailablePetsAsync();
        Task DeletePetAsync(long petId);
        Task AddPetAsync(PetResource petResource);
    }
}
