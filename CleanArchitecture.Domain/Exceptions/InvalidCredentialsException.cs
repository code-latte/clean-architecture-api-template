namespace CleanArchitecture.Domain.Exceptions
{
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException(string usernameOrEmail)
            : base($"Invalid credentials for user {usernameOrEmail}.") { }
    }
}
