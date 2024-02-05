namespace CleanArchitecture.Domain.Interfaces.Configuration
{
    public interface IEmailConfiguration
    {
        string SmtpServer { get; set; }
        int SmtpPort { get; set; }
        bool SmtpEnableSsl { get; set; }
        string SmtpUsername { get; set; }
        string SmtpPassword { get; set; }
        string SmtpFrom { get; set; }
        string InternalMailName { get; set; }
        string InternalMailAddress { get; set; }
    }
}
