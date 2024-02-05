using Dapper.Contrib.Extensions;

namespace CleanArchitecture.Domain.Entities
{
    [Table("Accounts")]
    public class Account
    {
        [ExplicitKey]
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public bool IsActive { get; set; }
    }
}
