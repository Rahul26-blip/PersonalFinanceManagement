using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PFM.Application.IServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PFM.Application.Services
{
    public class JWTServices : IJWTServices
    {
        private readonly IConfiguration _configuration;

        public JWTServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateToken(int userId, string userName, string userCode)
        {
            // Read JWT settings from appsettings.json
            var key = _configuration["Jwt:Key"];
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];
            var expireMinutes = _configuration.GetValue<int>("Jwt:ExpireMinutes");

            // Check configuration
            if (string.IsNullOrWhiteSpace(key))
            throw new InvalidOperationException("JWT Key is missing.");

            if (string.IsNullOrWhiteSpace(issuer))
            throw new InvalidOperationException("JWT Issuer is missing.");

            if (string.IsNullOrWhiteSpace(audience))
            throw new InvalidOperationException("JWT Audience is missing.");

            if (expireMinutes <= 0)expireMinutes = 120;

            // Create signing key
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var credentials = new SigningCredentials( securityKey,SecurityAlgorithms.HmacSha256);

            // Add user information to the token
            var claims = new List<Claim>
            {
                new Claim("UserId", userId.ToString()),
                new Claim("UserName", userName),
                new Claim("UserCode", userCode)
            };

            // Create token with matching issuer and audience

            var token = new JwtSecurityToken(issuer: issuer,audience: audience,claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expireMinutes),
                signingCredentials: credentials
            );

            // Return token as a string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}