namespace CleanArchitecture.Domain.Exceptions
{
    public class TokenNotValidException : Exception
    {
        public TokenNotValidException(string token)
            : base($"Token {token} is not valid for given user or has expired.") { }
    }
}
