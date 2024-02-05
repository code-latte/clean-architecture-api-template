namespace CleanArchitecture.Domain.Interfaces.Security
{
    public interface ITokenGenerator
    {
        string GenerateJWTToken(string accountId);
        string GenerateAuthorizationToken();
        string GenerateLeaguePassword();
    }
}
