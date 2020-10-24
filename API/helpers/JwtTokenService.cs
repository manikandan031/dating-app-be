using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.helpers
{
    public class JwtTokenService
    {
        private readonly string _jwtSecret;
        
        public JwtTokenService(IConfiguration config)
        {
            _jwtSecret = config["Jwt:SecretKey"];
            Console.WriteLine(@"secret {0}", _jwtSecret);
        }

        public string CreateToken(AppUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name), 
            };
            var token = new JwtSecurityToken(
                claims: claims,
                signingCredentials: credentials,
                expires: DateTime.Today.AddDays(7),
                issuer: "Manikandan",
                audience: "Audience"
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}