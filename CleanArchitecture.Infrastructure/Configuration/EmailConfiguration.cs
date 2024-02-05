using Microsoft.Extensions.Configuration;
using CleanArchitecture.Domain.Interfaces.Configuration;

namespace CleanArchitecture.Infrastructure.Configuration
{
    public class EmailConfiguration : IEmailConfiguration
    {
        public string SmtpServer { get; set; }
        public int SmtpPort { get; set; }
        public bool SmtpEnableSsl { get; set; }
        public string SmtpUsername { get; set; }
        public string SmtpPassword { get; set; }
        public string SmtpFrom { get; set; }
        public string InternalMailName { get; set; }
        public string InternalMailAddress { get; set; }

        public EmailConfiguration(IConfiguration configuration)
        {
            SmtpServer = configuration.GetSection("Email")["Host"];
            SmtpPort = int.Parse(configuration.GetSection("Email")["Port"]);
            SmtpEnableSsl = bool.Parse(configuration.GetSection("Email")["EnableSsl"]);
            SmtpUsername = configuration.GetSection("Email")["Username"];
            SmtpPassword = configuration.GetSection("Email")["Password"];
            SmtpFrom = configuration.GetSection("Email")["From"];
            InternalMailName = configuration.GetSection("Email")["InternalMailName"];
            InternalMailAddress = configuration.GetSection("Email")["InternalMailAddress"];
        }
    }
}
