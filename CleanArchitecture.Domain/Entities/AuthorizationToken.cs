using Dapper.Contrib.Extensions;

namespace CleanArchitecture.Domain.Entities
{
    [Table("AuthorizationTokens")]
    public class AuthorizationToken
    {
        [Key]
        public int Id { get; set; }
        public string Token { get; set; }
        public string AccountId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UsedAt { get; set; }
    }
}
