namespace Domain.DTO.Responses
{
    public class OrderCreateResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}