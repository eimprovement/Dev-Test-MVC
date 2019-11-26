using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eimprovement.Domain.Models;

namespace eimprovement.Domain.Services
{
    public class MockDataSource : IPetStoreData
    {
        public MockDataSource()
        {

        }

        public long Add(Pet pet)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pet> FindByStatus(string status)
        {
            throw new NotImplementedException();
        }

        public Pet Get(long id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Pet> GetAll()
        {
            throw new NotImplementedException();
        }

        public void UpdatePet(long petId)
        {
            throw new NotImplementedException();
        }
    }
}
