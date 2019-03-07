using System.ComponentModel.DataAnnotations;

namespace eimprovement.WebApplication.Models
{
    public class PetViewModel
    {
        [Display(Name = "Pet Name") ]
        public string Name { get; set; }

        public long Id { get; set; }
    }
}