using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using CleanArchitecture.Domain.Interfaces.Configuration;
using CleanArchitecture.Domain.Interfaces.Security;

namespace CleanArchitecture.Infrastructure.Security
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IJWTConfiguration _JWTConfiguration;

        public TokenGenerator(IJWTConfiguration JWTConfiguration)
        {
            _JWTConfiguration = JWTConfiguration;
        }

        public string GenerateAuthorizationToken()
        {
            // generate 6 digit random code
            var random = new Random();
            var code = random.Next(100000, 999999);
            return code.ToString();
        }

        public string GenerateJWTToken(string accountId)
        {
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("Id", accountId) }),
                Expires = DateTime.Now.AddMinutes(3600),
                Issuer = _JWTConfiguration.Issuer,
                Audience = _JWTConfiguration.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_JWTConfiguration.Key)),
                    SecurityAlgorithms.HmacSha512Signature
                )
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);

            return stringToken;
        }

        public string GenerateLeaguePassword()
        {
            // Define the character set for the password
            var chars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
            var stringChars = new char[8];
            var random = new Random();

            // Generate a random password with 8 characters
            for (int i = 0; i < 8; i++)
            {
                int index = random.Next(0, chars.Length);
                stringChars[i] = chars[index];
            }

            // Convert the character array to a string
            var password = new string(stringChars);
            return password;
        }
    }
}
