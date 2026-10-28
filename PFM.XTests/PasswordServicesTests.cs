
using Xunit;
using PFM.Application.Services;

namespace PFM.XTests
{
    public class PasswordServicesTests
    {
        private readonly PasswordServices _passwordServices;

        public PasswordServicesTests()
        {
            // Create the real password service.
            _passwordServices = new PasswordServices();
        }

        // TEST 1: HashPassword should generate a hash.
        [Fact]
        public void HashPassword_WhenPasswordIsValid_ShouldReturnHash()
        {
            // Arrange
            string password = "TestPassword123!";

            // Act
            string passwordHash =
                _passwordServices.HashPassword(password);

            // Assert
            Assert.NotNull(passwordHash);
            Assert.NotEmpty(passwordHash);
            Assert.NotEqual(password, passwordHash);
        }

        // TEST 2: Correct password should be verified.
        [Fact]
        public void VerifyPassword_WhenPasswordIsCorrect_ShouldReturnTrue()
        {
            // Arrange
            string password = "TestPassword123!";

            string passwordHash =
                _passwordServices.HashPassword(password);

            // Act
            bool result = _passwordServices.VerifyPassword(
                password,
                passwordHash);

            // Assert
            Assert.True(result);
        }

        // TEST 3: Incorrect password should be rejected.
        [Fact]
        public void VerifyPassword_WhenPasswordIsIncorrect_ShouldReturnFalse()
        {
            // Arrange
            string correctPassword = "TestPassword123!";

            string passwordHash =
                _passwordServices.HashPassword(correctPassword);

            string incorrectPassword = "WrongPassword123!";

            // Act
            bool result = _passwordServices.VerifyPassword(
                incorrectPassword,
                passwordHash);

            // Assert
            Assert.False(result);
        }

        // TEST 4: Empty password should be rejected.
        [Fact]
        public void VerifyPassword_WhenPasswordIsEmpty_ShouldReturnFalse()
        {
            // Arrange
            string password = "";
            string passwordHash =
                _passwordServices.HashPassword("TestPassword123!");

            // Act
            bool result = _passwordServices.VerifyPassword(
                password,
                passwordHash);

            // Assert
            Assert.False(result);
        }

        // TEST 5: Empty hash should be rejected.
        [Fact]
        public void VerifyPassword_WhenHashIsEmpty_ShouldReturnFalse()
        {
            // Arrange
            string password = "TestPassword123!";
            string passwordHash = "";

            // Act
            bool result = _passwordServices.VerifyPassword(
                password,
                passwordHash);

            // Assert
            Assert.False(result);
        }

        // TEST 6: A different password must not match the hash.
        [Fact]
        public void VerifyPassword_WhenDifferentPasswordIsUsed_ShouldReturnFalse()
        {
            // Arrange
            string originalPassword = "OriginalPassword123!";

            string passwordHash =
                _passwordServices.HashPassword(originalPassword);

            // Act
            bool result = _passwordServices.VerifyPassword(
                "AnotherPassword123!",
                passwordHash);

            // Assert
            Assert.False(result);
        }
    }
}
