using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eimprovement.WebApplication.Models
{
    public class PetShop
    {
        public long Id { get; set; }
        [Required]
        public string Name { get; set; }
        public Category Category { get; set; }
        public PhotoUrl PhotoUrl { get; set; }
        public Tags TagParent { get; set; }
        public string Status { get; set; }
    }

    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class PhotoUrl
    {
        public List<string> PhotoLink { get; set; }
    }

    public class Tags
    {
        public List<Tag> Tag { get; set; }
    }

    public class Tag
    {
        public long Id { get; set; }
        public int Name { get; set; }
    }
}