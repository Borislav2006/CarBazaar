using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CarBazaar.Interface;
using CarBazaar.Models;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Pqc.Crypto.Crystals.Dilithium;

namespace CarBazaar.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;
        private readonly SymmetricSecurityKey securityKey;
        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SigningKey"]));
            //_key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
        }
        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };

            var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = configuration["JWT:Issuer"],
                Audience = configuration["JWT:Audience"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(TokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
