using Microsoft.Extensions.Configuration;
using CleanArchitecture.Domain.Interfaces.Configuration;

namespace CleanArchitecture.Infrastructure.Configuration
{
    public class JWTConfiguration : IJWTConfiguration
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }

        public JWTConfiguration(IConfiguration configuration)
        {
            Key = configuration.GetSection("JWT")["Key"];
            Issuer = configuration.GetSection("JWT")["Issuer"];
            Audience = configuration.GetSection("JWT")["Audience"];
        }
    }
}
