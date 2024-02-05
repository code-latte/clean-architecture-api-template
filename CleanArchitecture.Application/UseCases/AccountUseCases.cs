using CleanArchitecture.Application.DTO.Responses;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Exceptions;
using CleanArchitecture.Domain.Interfaces.Data;

namespace CleanArchitecture.Application.UseCases
{
    public class AccountUseCases : IAccountUseCases
    {
        private readonly IRepository<Account, string> _accountRepository;

        public AccountUseCases(IRepository<Account, string> accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public Task DeleteAccountAsync(string accountId)
        {
            throw new NotImplementedException();
        }

        public async Task<AccountResponse> GetAccountAsync(string accountId)
        {
            Account account = await _accountRepository.GetByIdAsync(accountId);
            if (account == null)
            {
                throw new EntityNotFoundException("Account", accountId);
            }

            return new AccountResponse
            {
                AccountId = account.Id,
                Username = account.Username,
                Email = account.Email,
            };
        }
    }
}
