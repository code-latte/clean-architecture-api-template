namespace CleanArchitecture.Application.DTO.Requests
{
    public class ConfirmPasswordResetRequest
    {
        public string UsernameOrEmail { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
