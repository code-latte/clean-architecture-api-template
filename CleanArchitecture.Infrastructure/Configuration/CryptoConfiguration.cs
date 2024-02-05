using Microsoft.Extensions.Configuration;
using CleanArchitecture.Domain.Interfaces.Configuration;

namespace CleanArchitecture.Infrastructure.Configuration
{
    public class CryptoConfiguration : ICryptoConfiguration
    {
        public string AesKey { get; set; }

        public CryptoConfiguration(IConfiguration configuration)
        {
            AesKey = configuration.GetSection("Crypto")["AesKey"];
        }
    }
}
