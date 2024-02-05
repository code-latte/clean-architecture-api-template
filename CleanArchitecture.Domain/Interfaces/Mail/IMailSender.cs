namespace CleanArchitecture.Domain.Interfaces.Mail
{
    public interface IMailSender
    {
        Task SendAsync(string to, string toMail, string subject, string html);
    }
}
