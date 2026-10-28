using Microsoft.Extensions.Configuration;
using PFM.Application.Services;
using System.IdentityModel.Tokens.Jwt;

namespace PFM.XTests
{
    public class JWTServicesTests
    {
        // Create a service with test JWT configuration.
        private JWTServices CreateService(
            string key = "ThisIsATestKeyForJWTSigning1234567890",
            string issuer = "PFM.Test",
            string audience = "PFM.TestUsers",
            string expireMinutes = "60")
        {
            var settings = new System.Collections.Generic.Dictionary<string, string?>
            {
                ["Jwt:Key"] = key,
                ["Jwt:Issuer"] = issuer,
                ["Jwt:Audience"] = audience,
                ["Jwt:ExpireMinutes"] = expireMinutes
            };

            IConfiguration configuration = new ConfigurationBuilder().AddInMemoryCollection(settings).Build();

            return new JWTServices(configuration);
        }

        // TEST 1: GenerateToken returns a non-empty token.
        [Fact]
        public void GenerateToken_WithValidSettings_ReturnsToken()
        {
            // Arrange
            var service = CreateService();

            // Act
            string token = service.GenerateToken( 1, "Rahul", "USR001");

            // Assert
            Assert.NotNull(token);
            Assert.NotEmpty(token);
        }

        // TEST 2: Token contains the expected user claims.
        [Fact]
        public void GenerateToken_WithValidUser_ContainsUserClaims()
        {
            // Arrange
            var service = CreateService();

            // Act
            string token = service.GenerateToken(1, "Rahul", "USR001");

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(token);

            // Assert
            Assert.Equal("1", jwt.Claims.First(c => c.Type == "UserId").Value);

            Assert.Equal("Rahul", jwt.Claims.First(c => c.Type == "UserName").Value);

            Assert.Equal("USR001", jwt.Claims.First(c => c.Type == "UserCode").Value);
        }

        // TEST 3: Token contains the configured issuer and audience.
        [Fact]
        public void GenerateToken_WithValidSettings_ContainsIssuerAndAudience()
        {
            // Arrange
            var service = CreateService();

            // Act
            string token = service.GenerateToken(1, "Rahul", "USR001");

            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);

            // Assert
            Assert.Equal("PFM.Test", jwt.Issuer);
            Assert.Contains("PFM.TestUsers", jwt.Audiences);
        }

        // TEST 4: Token has an expiration in the future.
        [Fact]
        public void GenerateToken_WithValidSettings_HasFutureExpiration()
        {
            // Arrange
            var service = CreateService();

            // Act
            string token = service.GenerateToken(1, "Rahul", "USR001");

            var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);

            // Assert
            Assert.True(jwt.ValidTo > DateTime.UtcNow);
        }

        // TEST 5: Missing key throws InvalidOperationException.
        [Fact]
        public void GenerateToken_WhenKeyIsMissing_ThrowsException()
        {
            // Arrange
            var service = CreateService(key: "");

            // Act and Assert
            var exception = Assert.Throws<InvalidOperationException>(
                () => service.GenerateToken(1, "Rahul", "USR001"));

            Assert.Equal("JWT Key is missing.", exception.Message);
        }

        // TEST 6: Missing issuer throws InvalidOperationException.
        [Fact]
        public void GenerateToken_WhenIssuerIsMissing_ThrowsException()
        {
            // Arrange
            var service = CreateService(issuer: "");

            // Act and Assert
            var exception = Assert.Throws<InvalidOperationException>(
                () => service.GenerateToken(1, "Rahul", "USR001"));

            Assert.Equal("JWT Issuer is missing.", exception.Message);
        }

        // TEST 7: Missing audience throws InvalidOperationException.
        [Fact]
        public void GenerateToken_WhenAudienceIsMissing_ThrowsException()
        {
            // Arrange
            var service = CreateService(audience: "");

            // Act and Assert
            var exception = Assert.Throws<InvalidOperationException>(
                () => service.GenerateToken(1, "Rahul", "USR001"));

            Assert.Equal("JWT Audience is missing.", exception.Message);
        }
    }
}
