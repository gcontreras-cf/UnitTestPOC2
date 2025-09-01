namespace Domain.DTO.Responses
{
    public class TodayOrderResponse
    {
        public string ClientName { get; set; } = string.Empty;
        public List<TodayOrderDetailResponse> OrderDetails { get; set; } = new List<TodayOrderDetailResponse>();
    }

    public class TodayOrderDetailResponse
    {
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}   