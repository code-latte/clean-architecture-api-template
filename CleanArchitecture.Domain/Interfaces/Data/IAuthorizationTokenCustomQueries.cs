using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Interfaces.Data
{
    public interface IAuthorizationTokenCustomQueries
    {
        Task<AuthorizationToken> GetByTokenAndAccountIdAsync(string token, string accountId);
    }
}
