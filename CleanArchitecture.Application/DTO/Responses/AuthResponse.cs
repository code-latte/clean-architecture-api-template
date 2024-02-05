namespace CleanArchitecture.Application.DTO.Responses
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public string AccountId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
