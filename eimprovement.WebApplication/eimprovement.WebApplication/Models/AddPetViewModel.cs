using System.ComponentModel.DataAnnotations;

namespace eimprovement.WebApplication.Models
{
    public class AddPetViewModel
    {
        [Display(Name = "Name")]
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
    }
}