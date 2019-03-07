using System.ComponentModel.DataAnnotations;

namespace eimprovement.WebApplication.Models
{
    public class UpdatePetViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Name")]
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
    }
}