namespace Domain.DTO.Requests
{
    public class ProductUpdateRequest
    {
        public int ProductSeq { get; set; } // Identificador único del producto
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}