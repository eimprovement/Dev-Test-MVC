namespace eimprovement.WebApplication.Client.Models
{
    public class PetResource
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        public static PetResource CreateNew(string name) {
            return new PetResource
            {
                Name = name,
                Status = Constants.PetStatusAvailable
            };
        }
    }
}