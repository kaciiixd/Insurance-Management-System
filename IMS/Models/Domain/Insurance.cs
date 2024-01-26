namespace IMS.Models.Domain
{
    public class Insurance
    {
        public Guid Id { get; set; }
        public string InsuranceType { get; set; }
        public int Sum { get; set; }
        public string Subject { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidUntil { get; set; }

        // Foreign key for Client
        public Guid ClientId { get; set; }
        public Client Client { get; set; }
    }
}
