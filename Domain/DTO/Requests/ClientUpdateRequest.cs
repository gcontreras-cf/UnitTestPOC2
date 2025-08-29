namespace Domain.DTO.Requests
{
    public class ClientUpdateRequest
    {
        public int ClientSeq { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}