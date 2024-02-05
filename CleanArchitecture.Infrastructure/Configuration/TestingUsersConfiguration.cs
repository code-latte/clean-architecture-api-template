using Microsoft.Extensions.Configuration;
using CleanArchitecture.Domain.Interfaces.Configuration;

namespace CleanArchitecture.Infrastructure.Configuration
{
    public class TestingUsersConfiguration : ITestingUsersConfiguration
    {
        public Dictionary<string, string> TestingUsers { get; set; }

        public TestingUsersConfiguration(IConfiguration configuration)
        {
            TestingUsers = new Dictionary<string, string>();
            var testingUsers = configuration.GetSection("TestingUsers");
            //section testingUsers is a aray of objects with properties email and token
            foreach (var testingUser in testingUsers.GetChildren())
            {
                TestingUsers.Add(testingUser["Email"], testingUser["Token"]);
            }
        }
    }
}
