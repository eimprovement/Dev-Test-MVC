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
        List<Pet> _pets;
        List<Category> _categories;
        List<Status> _statuses;

        public MockDataSource()
        {
            // This is for the Home page dropdown Status selector only
            InitializeStatus();

            InitializeCategories();
            InitializePets();
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

        public void UpdatePet(long petId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Status> GetAll()
        {
            return _statuses.OrderBy(s => s.id);
        }


        #region data store mock

        private void InitializeCategories()
        {
            _categories = new List<Category>()
            {
                new Category
                {
                    id = 1,
                    name = "doggie"
                },
                new Category
                {
                    id = 2,
                    name = "feline"
                },
                new Category
                {
                    id = 3,
                    name = "reptile"
                },
                new Category
                {
                    id = 4,
                    name = "bird"
                }
            };
        }

        private void InitializePets()
        {
            _pets = new List<Pet>()
                {
                    new Pet {
                        id = 1,
                        name = "Charlie",
                        category = new Category { id = 1, name = "Dog"},
                        photoUrls = new string[] {"cutedoggieurl"},
                        status = "Available",
                        tags = new[] { new Tag { id = 1, name = "cute" } }
                    },
                    new Pet {
                        id = 2,
                        name = "Elvis",
                        category = new Category { id = 1, name = "Dog" },
                        photoUrls = new string[] {"uglydogurl"},
                        status = "Sold",
                        tags = new[] { new Tag { id = 1, name = "ugly" } }
                    },
                    new Pet {
                        id = 3,
                        name = "Greta",
                        category = new Category { id =2, name = "Cat"},
                        photoUrls = new string[] {"quietcatsurl"},
                        status = "Hold",
                        tags = new[] { new Tag { id = 1, name = "quiet" } }
                    }
                };
        }

        private void InitializeStatus()
        {
            _statuses = new List<Status>()
            {
                new Status
                {
                    id = 1,
                    name = "available"
                },
                new Status
                {
                    id = 2,
                    name = "held"
                },
                new Status
                {
                    id = 3,
                    name = "sold"
                }
            };
        }
        
        #endregion
    }
}
