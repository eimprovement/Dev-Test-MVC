using System.Collections.Generic;
using System.Threading.Tasks;

using eimprovement.WebApplication.Models;

namespace eimprovement.WebApplication.Services
{
    public interface IPetStoreService
    {
        Task<IEnumerable<PetViewModel>> GetPetsAsync();
        Task AddPetAsync(AddPetViewModel model);
        Task<UpdatePetViewModel> FindPetForUpdateAsync(long petId);
        Task UpdatePetAsync(UpdatePetViewModel model);
        Task DeletePetAsync(long petId);
    }
}
