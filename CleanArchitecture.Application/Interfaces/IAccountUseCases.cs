using CleanArchitecture.Application.DTO.Responses;

namespace CleanArchitecture.Application.Interfaces
{
    public interface IAccountUseCases
    {
        Task<AccountResponse> GetAccountAsync(string accountId);
        Task DeleteAccountAsync(string accountId);
    }
}
