namespace CleanArchitecture.Domain.Interfaces.Mail
{
    public interface IMailTemplateRepository
    {
        Task<string> ComposeEmailAsync(string template, Dictionary<string, string> parameters);
    }
}
