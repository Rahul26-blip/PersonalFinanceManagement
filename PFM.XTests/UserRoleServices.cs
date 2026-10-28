
using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using PFM.Application.DTO;
using PFM.Application.Services;
using PFM.Domain.Entites;
using PFM.Infrastructure.IRepository;

namespace PFM.Tests
{
    public class UserRoleServicesTests
    {
        private readonly Mock<IUserRoleRepository> _repositoryMock;
        private readonly UserRoleServices _service;

        public UserRoleServicesTests()
        {
            _repositoryMock = new Mock<IUserRoleRepository>();
            _service = new UserRoleServices(_repositoryMock.Object);
        }

        [Fact]
        public void GetAll_ShouldReturnUserRoles()
        {
            var userRoles = new List<UserRole>
            {
                new UserRole
                {
                    UserRoleId = 1,
                    UserId = 10,
                    RoleId = 2,
                    IsActive = true
                }
            };

            _repositoryMock.Setup(x => x.GetAll())
                .Returns(userRoles);

            var result = _service.GetAll();

            Assert.Single(result);
            Assert.Equal(1, result[0].UserRoleId);
            Assert.Equal(10, result[0].UserId);
            Assert.Equal(2, result[0].RoleId);
        }

        [Fact]
        public void GetAll_WhenNoRecords_ShouldReturnEmptyList()
        {
            _repositoryMock.Setup(x => x.GetAll())
                .Returns(new List<UserRole>());

            var result = _service.GetAll();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Fact]
        public void GetById_WhenRecordExists_ShouldReturnDTO()
        {
            var userRole = new UserRole
            {
                UserRoleId = 1,
                UserId = 10,
                RoleId = 2,
                IsActive = true
            };

            _repositoryMock.Setup(x => x.GetById(1))
                .Returns(userRole);

            var result = _service.GetById(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.UserRoleId);
            Assert.Equal(10, result.UserId);
            Assert.Equal(2, result.RoleId);
        }

        [Fact]
        public void GetById_WhenRecordDoesNotExist_ShouldReturnNull()
        {
            _repositoryMock.Setup(x => x.GetById(99))
                .Returns((UserRole)null);

            var result = _service.GetById(99);

            Assert.Null(result);
        }

        [Fact]
        public void Create_ShouldMapDTOAndCallRepository()
        {
            var dto = new UserRoleDTO
            {
                UserId = 10,
                RoleId = 2,
                IsActive = true,
                CreatedOn = DateTime.Now
            };

            _service.Create(dto);

            _repositoryMock.Verify(x => x.Create(
                It.Is<UserRole>(u =>
                    u.UserId == dto.UserId &&
                    u.RoleId == dto.RoleId &&
                    u.IsActive == dto.IsActive)),
                Times.Once);
        }

        [Fact]
        public void Create_ShouldPreserveUserIdAndRoleId()
        {
            var dto = new UserRoleDTO
            {
                UserId = 25,
                RoleId = 4,
                IsActive = true
            };

            _service.Create(dto);

            _repositoryMock.Verify(x => x.Create(
                It.Is<UserRole>(u =>
                    u.UserId == 25 &&
                    u.RoleId == 4)),
                Times.Once);
        }

        [Fact]
        public void Update_WhenRecordExists_ShouldUpdateExistingEntity()
        {
            // Arrange: create the existing entity.
            var existingUserRole = new UserRole
            {
                UserRoleId = 5,
                UserId = 10,
                RoleId = 2,
                IsActive = true
            };

            // Arrange: provide the new values.
            var dto = new UserRoleDTO
            {
                UserRoleId = 5,
                UserId = 20,
                RoleId = 3,
                IsActive = false
            };

            _repositoryMock.Setup(x => x.GetById(5))
                .Returns(existingUserRole);

            // Act: update the existing entity.
            _service.Update(dto);

            // Assert: check the actual entity was changed.
            Assert.Equal(5, existingUserRole.UserRoleId);
            Assert.Equal(20, existingUserRole.UserId);
            Assert.Equal(3, existingUserRole.RoleId);
            Assert.False(existingUserRole.IsActive);
            Assert.NotNull(existingUserRole.UpdatedOn);

            // Assert: repository Update was called once.
            _repositoryMock.Verify(
                x => x.Update(It.IsAny<UserRole>()),
                Times.Once);
        }

        [Fact]
        public void Update_WhenRecordDoesNotExist_ShouldThrowException()
        {
            var dto = new UserRoleDTO
            {
                UserRoleId = 99,
                UserId = 10,
                RoleId = 2,
                IsActive = true
            };

            _repositoryMock.Setup(x => x.GetById(99))
                .Returns((UserRole)null);

            Assert.Throws<KeyNotFoundException>(
                () => _service.Update(dto));

            _repositoryMock.Verify(
                x => x.Update(It.IsAny<UserRole>()),
                Times.Never);
        }

        [Fact]
        public void Update_ShouldSetUpdatedOn()
        {
            var existingUserRole = new UserRole
            {
                UserRoleId = 5,
                UserId = 10,
                RoleId = 2,
                IsActive = true
            };

            var dto = new UserRoleDTO
            {
                UserRoleId = 5,
                UserId = 10,
                RoleId = 2,
                IsActive = true
            };

            _repositoryMock.Setup(x => x.GetById(5))
                .Returns(existingUserRole);

            _service.Update(dto);

            // Check the timestamp on the actual existing entity.
            Assert.NotNull(existingUserRole.UpdatedOn);

            _repositoryMock.Verify(
                x => x.Update(It.IsAny<UserRole>()),
                Times.Once);
        }

        [Fact]
        public void Delete_ShouldCallRepository()
        {
            _service.Delete(5);

            _repositoryMock.Verify(
                x => x.Delete(5),
                Times.Once);
        }
    }
}
