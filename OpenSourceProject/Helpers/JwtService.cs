
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OpenSourceProject.Areas.Client.Models.ResourceModels;
using OpenSourceProject.Areas.Provider.Models.ResourceModels;
using OpenSourceProject.Models;

namespace OpenSourceProject.Helpers
{
    public class JwtService
    {
        private const int EXPIRATION_MINUTES = 60;
        private readonly IConfiguration _configuration;
         public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<AuthenticationClientResponse> CreateTokenForClient(Client user,string role)
        {
            var expiration = DateTime.UtcNow.AddMinutes(EXPIRATION_MINUTES);
            var token = CreateJwtToken(CreateClaims(user,role), CreateSigningCredentials(), expiration);
            var tokenHandler = new JwtSecurityTokenHandler();

            return new AuthenticationClientResponse
            {
                Token = tokenHandler.WriteToken(token),
                Expiration = expiration,
                User= user
            };
        }
        public async Task<AuthenticationProviderResponse> CreateTokenForProvider(Provider user, string role)
        {
            var expiration = DateTime.UtcNow.AddMinutes(EXPIRATION_MINUTES);
            var token = CreateJwtToken(CreateClaims(user, role), CreateSigningCredentials(), expiration);
            var tokenHandler = new JwtSecurityTokenHandler();

            return new AuthenticationProviderResponse
            {
                Token = tokenHandler.WriteToken(token),
                Expiration = expiration,
                User = user
            };
        }


        private JwtSecurityToken CreateJwtToken(Claim[] claims, SigningCredentials credentials, DateTime expiration)
            => new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: expiration,
                signingCredentials: credentials);

        private Claim[] CreateClaims(ApplicationUser user, string role)
            => new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Name??"no name"),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                new Claim(ClaimTypes.Role,role)
            };
        private SigningCredentials CreateSigningCredentials()
            => new SigningCredentials(
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
                ),
                SecurityAlgorithms.HmacSha256);
    }
}
