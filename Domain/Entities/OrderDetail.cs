    namespace Domain.Entities
{
    public class OrderDetail
    {
        public int OrderDetailSeq { get; set; } // Primary Key

        // Foreign Keys
        public int OrderSeq { get; set; }
        public Order Order { get; set; } = new Order();

        public int ProductSeq { get; set; }
        public Product Product { get; set; } = new Product();

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // New field
        public bool Active { get; set; } = true;
    }

}