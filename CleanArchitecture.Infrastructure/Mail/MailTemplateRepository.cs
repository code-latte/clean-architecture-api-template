using Microsoft.AspNetCore.Hosting;
using CleanArchitecture.Domain.Interfaces.Mail;

namespace CleanArchitecture.Infrastructure.Mail
{
    public class MailTemplateRepository : IMailTemplateRepository
    {
        private Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;

        public MailTemplateRepository(
            Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostingEnvironment
        )
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<string> ComposeEmailAsync(
            string template,
            Dictionary<string, string> parameters
        )
        {
            var templatePath = Path.Combine(
                _hostingEnvironment.WebRootPath,
                "static",
                template + ".html"
            );
            var html = await File.ReadAllTextAsync(templatePath);
            foreach (var parameter in parameters)
            {
                html = html.Replace($"[{parameter.Key}]", parameter.Value);
            }
            return html;
        }
    }
}
