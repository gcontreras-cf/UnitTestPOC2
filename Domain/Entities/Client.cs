namespace Domain.Entities
{
    public class Client
    {
        public int ClientSeq { get; set; } // Primary Key
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Navigation property
        public ICollection<Order> Orders { get; set; } = new List<Order>();

        // New field
        public bool Active { get; set; } = true;
    }

}