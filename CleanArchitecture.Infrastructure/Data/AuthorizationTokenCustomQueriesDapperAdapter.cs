using Dapper;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Interfaces.Configuration;
using CleanArchitecture.Domain.Interfaces.Data;
using Microsoft.Data.SqlClient;

namespace CleanArchitecture.Infrastructure.Data
{
    public class AuthorizationTokenCustomQueriesDapperAdapter : IAuthorizationTokenCustomQueries
    {
        protected readonly SqlConnection readerConnection;
        protected readonly SqlConnection writerConnection;

        public AuthorizationTokenCustomQueriesDapperAdapter(IConnectionStrings connectionStrings)
        {
            readerConnection = new SqlConnection(connectionStrings.ReaderConnection);
            writerConnection = new SqlConnection(connectionStrings.WriterConnection);
        }

        public async Task<AuthorizationToken> GetByTokenAndAccountIdAsync(
            string token,
            string accountId
        )
        {
            AuthorizationToken authToken = (
                await readerConnection.QueryAsync<AuthorizationToken>(
                    "SELECT * FROM AuthorizationTokens WHERE Token = @Token AND AccountId = @AccountId and UsedAt IS NULL",
                    new { Token = token, AccountId = accountId }
                )
            ).FirstOrDefault();

            return authToken;
        }
    }
}
