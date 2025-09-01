namespace Domain.DTO.Requests
{
    public class OrderCreateRequest
    {
        public int ClientSeq { get; set; }
        public List<OrderDetailRequest> OrderDetails { get; set; } = new List<OrderDetailRequest>();
    }

    public class OrderDetailRequest
    {
        public int ProductSeq { get; set; }
        public int Quantity { get; set; }
    }
}