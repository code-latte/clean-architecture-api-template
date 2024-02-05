namespace CleanArchitecture.Domain.Exceptions
{
    public class UsernameAlreadyInUseException : Exception
    {
        public UsernameAlreadyInUseException(string username)
            : base($"Username {username} is already in use.") { }
    }
}
