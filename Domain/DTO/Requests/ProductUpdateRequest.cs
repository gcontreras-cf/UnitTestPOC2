namespace Domain.DTO.Requests
{
    public class ProductUpdateRequest
    {
        public int ProductSeq { get; set; } // Identificador �nico del producto
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}