    namespace Domain.Entities
{
    public class Product
    {
        public int ProductSeq { get; set; } // Primary Key
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }

        // New field
        public bool Active { get; set; } = true;
    }
}
