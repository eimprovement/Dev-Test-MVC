using System.Collections.Generic;
using System.Threading.Tasks;

using eimprovement.WebApplication.Client.Models;

namespace eimprovement.WebApplication.Client
{
    public interface IPetStoreApiClient
    {
        Task<IEnumerable<PetResource>> GetAvailablePetsAsync();
        Task<PetResource> FindPetByIdAsync(long petId);
        Task AddPetAsync(PetResource pet);
        Task UpdatePetAsync(PetResource pet);
        Task DeletePetAsync(long petId);
    }
}
