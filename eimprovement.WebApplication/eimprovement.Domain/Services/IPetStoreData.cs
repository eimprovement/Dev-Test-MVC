using eimprovement.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eimprovement.Domain.Services
{
    public interface IPetStoreData
    {
        IEnumerable<Pet> GetAll();
        Pet Get(Int64 id);
        IEnumerable<Pet> FindByStatus(string status);
        Int64 Add(Pet pet);
        void UpdatePet(Int64 petId);
        void Delete(Int64 id);
    }
}
