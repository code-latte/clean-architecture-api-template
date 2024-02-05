namespace CleanArchitecture.Application.DTO.Requests
{
    public class LoginRequest
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
