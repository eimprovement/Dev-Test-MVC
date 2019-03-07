using System.Collections.Generic;
using System.Threading.Tasks;

using eimprovement.WebApplication.Models;

namespace eimprovement.WebApplication.Services
{
    public interface IPetStoreService
    {
        Task<IEnumerable<PetViewModel>> GetPetsAsync();
        Task AddPetAsync(AddPetViewModel model);
        Task DeletePet(long id);
    }
}
