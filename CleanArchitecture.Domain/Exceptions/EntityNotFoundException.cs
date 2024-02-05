namespace CleanArchitecture.Domain.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entityType, string entityId)
            : base($"{entityType} with id {entityId} was not found.") { }
    }
}
