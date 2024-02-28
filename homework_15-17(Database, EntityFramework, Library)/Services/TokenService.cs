using Common.Services;
using AccessToDB;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services
{
    internal class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;

        public TokenService(IConfiguration configuration)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtInfo:JwtKey"] ?? throw new ArgumentNullException("TokenKey")));
        }
        public string GetToken(IUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames.Email, user.Email)
            };

            var signature = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
            var descr = new SecurityTokenDescriptor
            {
                SigningCredentials = signature,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1)
            };

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(descr);
            return handler.WriteToken(token);
        }
    }
}
