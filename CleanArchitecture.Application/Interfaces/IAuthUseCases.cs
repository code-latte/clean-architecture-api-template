using CleanArchitecture.Application.DTO.Requests;
using CleanArchitecture.Application.DTO.Responses;

namespace CleanArchitecture.Application.Interfaces
{
    public interface IAuthUseCases
    {
        Task<AuthResponse> LoginAsync(LoginRequest request);
        Task<AuthResponse> SignupAsync(SignupRequest request);
        Task DeleteAccountAsync(string accountId);
        Task RequestPasswordResetAsync(PasswordResetRequest request);
        Task ConfirmPasswordResetAsync(ConfirmPasswordResetRequest request);
    }
}
