namespace Domain.DTO.Requests
{
    public class ProductInsertRequest
    {
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}