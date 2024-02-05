using Dapper;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Configuration;
using CleanArchitecture.Domain.Interfaces.Data;
using Microsoft.Data.SqlClient;

namespace CleanArchitecture.Infrastructure.Data
{
    public class AccountCustomQueriesDapperAdapter : IAccountCustomQueries
    {
        protected readonly SqlConnection readerConnection;
        protected readonly SqlConnection writerConnection;

        public AccountCustomQueriesDapperAdapter(IConnectionStrings connectionStrings)
        {
            readerConnection = new SqlConnection(connectionStrings.ReaderConnection);
            writerConnection = new SqlConnection(connectionStrings.WriterConnection);
        }

        public async Task<Account> GetByEmailAsync(string email)
        {
            return (
                await readerConnection.QueryAsync<Account>(
                    "SELECT * FROM Accounts WHERE Email = @Email",
                    new { Email = email }
                )
            ).FirstOrDefault();
        }

        public Task<Account> GetByUsernameAsync(string username)
        {
            return readerConnection.QueryFirstOrDefaultAsync<Account>(
                "SELECT * FROM Accounts WHERE Username = @Username",
                new { Username = username }
            );
        }

        public async Task<Account> GetByUsernameOrEmailAsync(string usernameOrEmail)
        {
            return (
                await readerConnection.QueryAsync<Account>(
                    "SELECT * FROM Accounts WHERE Username = @UsernameOrEmail OR Email = @UsernameOrEmail",
                    new { UsernameOrEmail = usernameOrEmail }
                )
            ).FirstOrDefault();
        }
    }
}
