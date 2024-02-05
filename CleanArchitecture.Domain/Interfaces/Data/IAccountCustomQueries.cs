using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Interfaces.Data
{
    public interface IAccountCustomQueries
    {
        Task<Account> GetByUsernameOrEmailAsync(string usernameOrEmail);
        Task<Account> GetByUsernameAsync(string username);
        Task<Account> GetByEmailAsync(string email);
    }
}
