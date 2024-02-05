using MailKit.Net.Smtp;
using MimeKit;
using CleanArchitecture.Domain.Interfaces.Configuration;
using CleanArchitecture.Domain.Interfaces.Mail;

namespace CleanArchitecture.Infrastructure.Mail
{
    public class MailSender : IMailSender
    {
        private readonly IEmailConfiguration _emailConfiguration;

        public MailSender(IEmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public async Task SendAsync(string to, string toMail, string subject, string html)
        {
            var message = new MimeMessage();
            message.From.Add(
                new MailboxAddress(_emailConfiguration.SmtpFrom, _emailConfiguration.SmtpUsername)
            );
            message.To.Add(new MailboxAddress(to, toMail));
            message.Subject = subject;
            message.Body = new TextPart("html") { Text = html };

            var cancelToken = new CancellationTokenSource();
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(
                    _emailConfiguration.SmtpServer,
                    _emailConfiguration.SmtpPort,
                    _emailConfiguration.SmtpEnableSsl,
                    cancelToken.Token
                );
                await client.AuthenticateAsync(
                    _emailConfiguration.SmtpUsername,
                    _emailConfiguration.SmtpPassword,
                    cancelToken.Token
                );
                await client.SendAsync(message, cancelToken.Token);
                await client.DisconnectAsync(true, cancelToken.Token);
            }
        }
    }
}
