namespace CleanArchitecture.Domain.Exceptions
{
    public class EmailAlreadyInUseException : Exception
    {
        public EmailAlreadyInUseException(string email)
            : base($"Email {email} is already in use.") { }
    }
}
