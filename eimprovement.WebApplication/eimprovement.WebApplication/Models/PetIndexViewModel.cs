using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace eimprovement.WebApplication.Models
{
    public class PetIndexViewModel
    {
        private IEnumerable<PetViewModel> _allPets;

        public static PetIndexViewModel Create(
            int pageNumber,
            int pageSize,
            string newNameFilter,
            string currentNameFilter)
        {
            string filterToUse = null;

            if (newNameFilter != null)
            {
                pageNumber = 1;
                filterToUse = newNameFilter;
            }
            else
            {
                filterToUse = currentNameFilter;
            }

            return new PetIndexViewModel(pageNumber, pageSize, filterToUse);
        }

        private PetIndexViewModel(int pageNumber, int pageSize, string nameFilter)
        {
            AllPets = Enumerable.Empty<PetViewModel>();
            NameFilter = nameFilter;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; }

        public string NameFilter { get; }

        private int PageSize { get; }

        public IPagedList<PetViewModel> PagedPets {
            get
            {
                IEnumerable<PetViewModel> toPage = AllPets;

                if (!string.IsNullOrEmpty(NameFilter))
                {
                    toPage = AllPets.Where(p => p.Name != null && p.Name.Contains(NameFilter));
                }
                
                return toPage.ToPagedList(PageNumber, PageSize);
            }
        }        

        public IEnumerable<PetViewModel> AllPets
        {
            get => _allPets;
            set => _allPets = value ?? throw new ArgumentNullException();
        }
    }
}