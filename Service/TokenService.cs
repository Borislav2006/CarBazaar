using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CarBazaar.Interface;
using CarBazaar.Models;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Pqc.Crypto.Crystals.Dilithium;
using DotNetEnv;

namespace CarBazaar.Service
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey signingKey;
        private readonly string jwtIssuer;
        private readonly string jwtAudience;
        public TokenService()
        {
            this.signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT__SigningKey")));
            this.jwtIssuer = Environment.GetEnvironmentVariable("JWT__Issuer");
            this.jwtAudience = Environment.GetEnvironmentVariable("JWT__Audience");
        }
        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };

            var creds = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha512Signature);

            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = jwtIssuer,
                Audience = jwtAudience
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(TokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
