using IMS.Models.Domain;

namespace IMS.Models.ViewModels
{
    public class DetailClientRequest
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }


        // Collection of insurances for the client
        public List<Insurance> Insurances { get; set; }
    }
}
