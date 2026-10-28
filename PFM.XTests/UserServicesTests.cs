
using System;
using System.Collections.Generic;
using Moq;
using Xunit;

using PFM.Application.DTO;
using PFM.Application.IServices;
using PFM.Application.Services;
using PFM.Domain.Entites;
using PFM.Infrastructure.IRepository;

namespace PFM.XTests
{
    public class UserServicesTests
    {
        private readonly Mock<IUserRepository> _mockRepository;
        private readonly Mock<IPasswordServices> _mockPasswordServices;
        private readonly Mock<IJWTServices> _mockJwtServices;
        private readonly UserServices _userServices;

        public UserServicesTests()
        {
            // Create fake dependencies.
            _mockRepository = new Mock<IUserRepository>();
            _mockPasswordServices = new Mock<IPasswordServices>();
            _mockJwtServices = new Mock<IJWTServices>();

            // Inject fake dependencies into UserServices.
            _userServices = new UserServices(
                _mockRepository.Object,
                _mockPasswordServices.Object,
                _mockJwtServices.Object);
        }

        // TEST 1: GetAll returns users.
        [Fact]
        public void GetAll_WhenUsersExist_ReturnsUsers()
        {
            // Arrange
            var users = new List<User>
            {
                new User
                {
                    UserId = 1,
                    UserName = "Rahul",
                    UserCode = "USR001",
                    IsActive = true,
                    CreatedOn = new DateTime(2026, 1, 1)
                },
                new User
                {
                    UserId = 2,
                    UserName = "Amit",
                    UserCode = "USR002",
                    IsActive = true,
                    CreatedOn = new DateTime(2026, 1, 2)
                }
            };

            _mockRepository
                .Setup(r => r.GetAll())
                .Returns(users);

            // Act
            var result = _userServices.GetAll();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("Rahul", result[0].UserName);
            Assert.Equal("Amit", result[1].UserName);

            _mockRepository.Verify(
                r => r.GetAll(),
                Times.Once);
        }

        // TEST 2: GetAll returns an empty list.
        [Fact]
        public void GetAll_WhenNoUsersExist_ReturnsEmptyList()
        {
            // Arrange
            _mockRepository
                .Setup(r => r.GetAll())
                .Returns(new List<User>());

            // Act
            var result = _userServices.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        // TEST 3: GetAll maps user properties correctly.
        [Fact]
        public void GetAll_MapsUserPropertiesCorrectly()
        {
            // Arrange
            var createdOn = new DateTime(2026, 2, 1);
            var updatedOn = new DateTime(2026, 2, 5);

            _mockRepository
                .Setup(r => r.GetAll())
                .Returns(new List<User>
                {
                    new User
                    {
                        UserId = 5,
                        UserName = "Rahul",
                        UserCode = "USR005",
                        IsActive = true,
                        CreatedOn = createdOn,
                        UpdatedOn = updatedOn
                    }
                });

            // Act
            var result = _userServices.GetAll();

            // Assert
            var user = Assert.Single(result);

            Assert.Equal(5, user.UserId);
            Assert.Equal("Rahul", user.UserName);
            Assert.Equal("USR005", user.UserCode);
            Assert.True(user.IsActive);
            Assert.Equal(createdOn, user.CreatedOn);
            Assert.Equal(updatedOn, user.UpdatedOn);
        }

        // TEST 4: GetById returns an existing user.
        [Fact]
        public void GetById_WhenUserExists_ReturnsUser()
        {
            // Arrange
            var existingUser = new User
            {
                UserId = 1,
                UserName = "Rahul",
                UserCode = "USR001",
                IsActive = true
            };

            _mockRepository
                .Setup(r => r.GetById(1))
                .Returns(existingUser);

            // Act
            var result = _userServices.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.UserId);
            Assert.Equal("Rahul", result.UserName);
            Assert.Equal("USR001", result.UserCode);
        }


        // TEST 5: GetById throws when the user does not exist.
        [Fact]
        public void GetById_WhenUserDoesNotExist_ShouldThrowException()
        {
            // Arrange
            _mockRepository
                .Setup(r => r.GetById(99))
                .Returns((User)null);

            // Act and Assert
            Assert.Throws<KeyNotFoundException>(
                () => _userServices.GetById(99));

            // Confirm the repository was called once.
            _mockRepository.Verify(
                r => r.GetById(99),
                Times.Once);
        }


        // TEST 6: Create sends user data to the repository.
        [Fact]
        public void Create_WhenCalled_CreatesUser()
        {
            // Arrange
            var dto = new UserDTO
            {
                UserName = "Rahul",
                UserCode = "USR001",
                IsActive = true,
                CreatedOn = new DateTime(2026, 1, 1)
            };

            User savedUser = null;

            _mockRepository
                .Setup(r => r.Create(It.IsAny<User>()))
                .Callback<User>(u => savedUser = u);

            // Act
            _userServices.Create(dto);

            // Assert
            Assert.NotNull(savedUser);
            Assert.Equal("Rahul", savedUser.UserName);
            Assert.Equal("USR001", savedUser.UserCode);
            Assert.True(savedUser.IsActive);
            Assert.Equal(dto.CreatedOn, savedUser.CreatedOn);
            Assert.Null(savedUser.UpdatedOn);

            _mockRepository.Verify(
                r => r.Create(It.IsAny<User>()),
                Times.Once);
        }

        // TEST 7: Update modifies an existing user.
        [Fact]
        public void Update_WhenUserExists_UpdatesUser()
        {
            // Arrange
            var existingUser = new User
            {
                UserId = 1,
                UserName = "Old Name",
                UserCode = "OLD001",
                IsActive = true
            };

            var dto = new UserDTO
            {
                UserId = 1,
                UserName = "New Name",
                UserCode = "NEW001",
                IsActive = false
            };

            _mockRepository
                .Setup(r => r.GetById(1))
                .Returns(existingUser);

            // Act
            _userServices.Update(dto);

            // Assert
            Assert.Equal("New Name", existingUser.UserName);
            Assert.Equal("NEW001", existingUser.UserCode);
            Assert.False(existingUser.IsActive);
            Assert.NotNull(existingUser.UpdatedOn);

            _mockRepository.Verify(
                r => r.Update(existingUser),
                Times.Once);
        }

        //// TEST 8: Update throws KeyNotFoundException for a missing user.

        //[Fact]
        //public void GetById_WhenUserDoesNotExist_ShouldThrowException()
        //{
        //    // Arrange
        //    _mockRepository
        //        .Setup(r => r.GetById(99))
        //        .Returns((User)null);

        //    // Act and Assert
        //    Assert.Throws<KeyNotFoundException>(
        //        () => _userServices.GetById(99));

        //    // Confirm the repository was called once
        //    _mockRepository.Verify(
        //        r => r.GetById(99),
        //        Times.Once);
        //}


        // TEST 9: Delete passes the correct ID.
        [Fact]
        public void Delete_WhenCalled_PassesCorrectIdToRepository()
        {
            // Act
            _userServices.Delete(5);

            // Assert
            _mockRepository.Verify(
                r => r.Delete(5),
                Times.Once);
        }

        // TEST 10: Register hashes the password and creates a user.
        [Fact]
        public void Register_WhenPasswordsMatch_HashesPasswordAndCreatesUser()
        {
            // Arrange
            var dto = new RegisterUserDTO
            {
                UserName = "Rahul",
                UserCode = "USR001",
                Password = "TestPassword123!",
                ConfirmPassword = "TestPassword123!"
            };

            _mockPasswordServices
                .Setup(p => p.HashPassword("TestPassword123!"))
                .Returns("hashed-password");

            User savedUser = null;

            _mockRepository
                .Setup(r => r.Create(It.IsAny<User>()))
                .Callback<User>(u => savedUser = u);

            // Act
            _userServices.Register(dto);

            // Assert
            Assert.NotNull(savedUser);
            Assert.Equal("Rahul", savedUser.UserName);
            Assert.Equal("USR001", savedUser.UserCode);
            Assert.Equal("hashed-password", savedUser.PasswordHash);
            Assert.True(savedUser.IsActive);

            _mockPasswordServices.Verify(
                p => p.HashPassword("TestPassword123!"),
                Times.Once);

            _mockRepository.Verify(
                r => r.Create(It.IsAny<User>()),
                Times.Once);
        }

        // TEST 11: Register does not create a user when passwords differ.
        [Fact]
        public void Register_WhenPasswordsDoNotMatch_DoesNotCreateUser()
        {
            // Arrange
            var dto = new RegisterUserDTO
            {
                UserName = "Rahul",
                UserCode = "USR001",
                Password = "Password123!",
                ConfirmPassword = "DifferentPassword!"
            };

            // Act
            _userServices.Register(dto);

            // Assert
            _mockRepository.Verify(
                r => r.Create(It.IsAny<User>()),
                Times.Never);

            _mockPasswordServices.Verify(
                p => p.HashPassword(It.IsAny<string>()),
                Times.Never);
        }

        // TEST 12: Login succeeds with valid credentials.
        [Fact]
        public void Login_WhenCredentialsAreValid_ReturnsLoginResponse()
        {
            // Arrange
            var user = new User
            {
                UserId = 1,
                UserName = "Rahul",
                UserCode = "USR001",
                PasswordHash = "hashed-password",
                IsActive = true
            };

            var dto = new LoginDTO
            {
                UserCode = "USR001",
                Password = "Password123!"
            };

            _mockRepository
                .Setup(r => r.GetByUsercode("USR001"))
                .Returns(user);

            _mockPasswordServices
                .Setup(p => p.VerifyPassword(
                    "Password123!",
                    "hashed-password"))
                .Returns(true);

            _mockJwtServices
                .Setup(j => j.GenerateToken(1, "Rahul", "USR001"))
                .Returns("test-jwt-token");

            // Act
            var result = _userServices.Login(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.UserId);
            Assert.Equal("Rahul", result.UserName);
            Assert.Equal("USR001", result.UserCode);
            Assert.Equal("test-jwt-token", result.Token);

            _mockJwtServices.Verify(
                j => j.GenerateToken(1, "Rahul", "USR001"),
                Times.Once);
        }

        // TEST 13: Login fails when the user does not exist.
        [Fact]
        public void Login_WhenUserDoesNotExist_ReturnsNull()
        {
            // Arrange
            _mockRepository
                .Setup(r => r.GetByUsercode("UNKNOWN"))
                .Returns((User)null);

            var dto = new LoginDTO
            {
                UserCode = "UNKNOWN",
                Password = "Password123!"
            };

            // Act
            var result = _userServices.Login(dto);

            // Assert
            Assert.Null(result);

            _mockPasswordServices.Verify(
                p => p.VerifyPassword(
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Never);

            _mockJwtServices.Verify(
                j => j.GenerateToken(
                    It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Never);
        }

        // TEST 14: Login fails for an inactive user.
        [Fact]
        public void Login_WhenUserIsInactive_ReturnsNull()
        {
            // Arrange
            var user = new User
            {
                UserId = 1,
                UserName = "Rahul",
                UserCode = "USR001",
                PasswordHash = "hashed-password",
                IsActive = false
            };

            _mockRepository
                .Setup(r => r.GetByUsercode("USR001"))
                .Returns(user);

            // Act
            var result = _userServices.Login(new LoginDTO
            {
                UserCode = "USR001",
                Password = "Password123!"
            });

            // Assert
            Assert.Null(result);

            _mockPasswordServices.Verify(
                p => p.VerifyPassword(
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Never);
        }

        // TEST 15: Login fails when the password is incorrect.
        [Fact]
        public void Login_WhenPasswordIsIncorrect_ReturnsNull()
        {
            // Arrange
            var user = new User
            {
                UserId = 1,
                UserName = "Rahul",
                UserCode = "USR001",
                PasswordHash = "hashed-password",
                IsActive = true
            };

            _mockRepository
                .Setup(r => r.GetByUsercode("USR001"))
                .Returns(user);

            _mockPasswordServices
                .Setup(p => p.VerifyPassword(
                    "WrongPassword!",
                    "hashed-password"))
                .Returns(false);

            // Act
            var result = _userServices.Login(new LoginDTO
            {
                UserCode = "USR001",
                Password = "WrongPassword!"
            });

            // Assert
            Assert.Null(result);

            _mockJwtServices.Verify(
                j => j.GenerateToken(
                    It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<string>()),
                Times.Never);
        }
    }
}
