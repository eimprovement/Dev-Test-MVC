namespace eimprovement.WebApplication.ViewModels
{
    public class PetViewModel
    {
        public long? Id { get; set; } = 0;
        public string Category { get; set; }
        public string  Name { get; set; }
        public string PhotoUrl { get; set; }
        public string[] Tags { get; set; }
    }
}