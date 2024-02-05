using Microsoft.Extensions.Configuration;
using CleanArchitecture.Domain.Interfaces.Configuration;

namespace CleanArchitecture.Infrastructure.Configuration
{
    public class ConnectionStrings : IConnectionStrings
    {
        public ConnectionStrings(IConfiguration configuration)
        {
            ReaderConnection = configuration.GetConnectionString("ReaderConnection");
            WriterConnection = configuration.GetConnectionString("WriterConnection");
            FootballLatteConnection = configuration.GetConnectionString("FootballLatteConnection");
        }

        public string ReaderConnection { get; set; }
        public string WriterConnection { get; set; }
        public string FootballLatteConnection { get; set; }
    }
}
