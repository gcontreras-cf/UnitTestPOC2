namespace Domain.Entities
{
    public class Order
    {
        public int OrderSeq { get; set; } // Primary Key
        public DateTime OrderDate { get; set; }

        // Foreign Key
        public int ClientSeq { get; set; }
        public Client Client { get; set; } = new Client();

        public decimal TotalAmount { get; set; }

        // Navigation property
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        // New field
        public bool Active { get; set; } = true;
    }
}
