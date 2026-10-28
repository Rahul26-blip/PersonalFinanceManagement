using Moq;
using PFM.Application.DTO;
using PFM.Application.Services;
using PFM.Domain.Entites;
using PFM.Infrastructure.IRepository;

namespace PFM.Tests
{
    public class PermissionServicesTests
    {
        private readonly Mock<IPermissionRepository> _repositoryMock;
        private readonly PermissionServices _service;

        public PermissionServicesTests()
        {
            _repositoryMock = new Mock<IPermissionRepository>();
            _service = new PermissionServices(
                _repositoryMock.Object);
        }

        // Test 1: GetAll returns permissions.
        [Fact]
        public void GetAll_WhenPermissionsExist_ShouldReturnPermissions()
        {
            var permissions = new List<Permission>
            {
                new Permission
                {
                    PermissionId = 1,
                    PermissionName = "Create Account",
                    PermissionCode = "ACCOUNT_CREATE",
                    IsActive = true
                },
                new Permission
                {
                    PermissionId = 2,
                    PermissionName = "View Account",
                    PermissionCode = "ACCOUNT_VIEW",
                    IsActive = true
                }
            };

            _repositoryMock.Setup(x => x.GetAll())
                .Returns(permissions);

            var result = _service.GetAll();

            Assert.Equal(2, result.Count);
            Assert.Equal("Create Account", result[0].PermissionName);
            Assert.Equal("ACCOUNT_VIEW", result[1].PermissionCode);
        }

        // Test 2: GetAll returns an empty list.
        [Fact]
        public void GetAll_WhenNoPermissionsExist_ShouldReturnEmptyList()
        {
            _repositoryMock.Setup(x => x.GetAll())
                .Returns(new List<Permission>());

            var result = _service.GetAll();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        // Test 3: GetById returns the permission.
        [Fact]
        public void GetById_WhenPermissionExists_ShouldReturnDTO()
        {
            var permission = new Permission
            {
                PermissionId = 1,
                PermissionName = "Create Account",
                PermissionCode = "ACCOUNT_CREATE",
                IsActive = true
            };

            _repositoryMock.Setup(x => x.GetById(1))
                .Returns(permission);

            var result = _service.GetById(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.PermissionId);
            Assert.Equal("Create Account", result.PermissionName);
            Assert.Equal("ACCOUNT_CREATE", result.PermissionCode);
            Assert.True(result.IsActive);
        }

        // Test 4: GetById returns null when missing.
        [Fact]
        public void GetById_WhenPermissionDoesNotExist_ShouldReturnNull()
        {
            _repositoryMock.Setup(x => x.GetById(99))
                .Returns((Permission)null);

            var result = _service.GetById(99);

            Assert.Null(result);
        }

        // Test 5: Create maps DTO values to the entity.
        [Fact]
        public void Create_ShouldMapDTOAndCallRepository()
        {
            var dto = new PermissionDTO
            {
                PermissionName = "Create Account",
                PermissionCode = "ACCOUNT_CREATE",
                IsActive = true
            };

            _service.Create(dto);

            _repositoryMock.Verify(
                x => x.Create(It.Is<Permission>(p =>
                    p.PermissionName == "Create Account" &&
                    p.PermissionCode == "ACCOUNT_CREATE" &&
                    p.IsActive)),
                Times.Once);
        }

        // Test 6: Update modifies the existing permission.
        [Fact]
        public void Update_WhenPermissionExists_ShouldUpdateExistingEntity()
        {
            var existingPermission = new Permission
            {
                PermissionId = 5,
                PermissionName = "Old Name",
                PermissionCode = "OLD_CODE",
                IsActive = true
            };

            var dto = new PermissionDTO
            {
                PermissionId = 5,
                PermissionName = "New Name",
                PermissionCode = "NEW_CODE",
                IsActive = false
            };

            _repositoryMock.Setup(x => x.GetById(5))
                .Returns(existingPermission);

            _service.Update(dto);

            Assert.Equal("New Name", existingPermission.PermissionName);
            Assert.Equal("NEW_CODE", existingPermission.PermissionCode);
            Assert.False(existingPermission.IsActive);
            Assert.NotNull(existingPermission.UpdatedOn);

            _repositoryMock.Verify(
                x => x.Update(It.IsAny<Permission>()),
                Times.Once);
        }

        // Test 7: Update throws when the permission is missing.
        [Fact]
        public void Update_WhenPermissionDoesNotExist_ShouldThrowException()
        {
            var dto = new PermissionDTO
            {
                PermissionId = 99,
                PermissionName = "Create Account",
                PermissionCode = "ACCOUNT_CREATE",
                IsActive = true
            };

            _repositoryMock.Setup(x => x.GetById(99))
                .Returns((Permission)null);

            Assert.Throws<KeyNotFoundException>(
                () => _service.Update(dto));

            _repositoryMock.Verify(
                x => x.Update(It.IsAny<Permission>()),
                Times.Never);
        }

        // Test 8: Delete calls the repository.
        [Fact]
        public void Delete_ShouldCallRepository()
        {
            _service.Delete(5);

            _repositoryMock.Verify(
                x => x.Delete(5),
                Times.Once);
        }

        // Test 9: GetAll maps all permission properties.
        [Fact]
        public void GetAll_ShouldMapPermissionProperties()
        {
            var permissions = new List<Permission>
            {
                new Permission
                {
                    PermissionId = 10,
                    PermissionName = "Edit Account",
                    PermissionCode = "ACCOUNT_EDIT",
                    IsActive = false
                }
            };

            _repositoryMock.Setup(x => x.GetAll())
                .Returns(permissions);

            var result = _service.GetAll();

            Assert.Single(result);
            Assert.Equal(10, result[0].PermissionId);
            Assert.Equal("Edit Account", result[0].PermissionName);
            Assert.Equal("ACCOUNT_EDIT", result[0].PermissionCode);
            Assert.False(result[0].IsActive);
        }

        // Test 10: Create sets the creation timestamp.
        [Fact]
        public void Create_ShouldSetCreatedOn()
        {
            var dto = new PermissionDTO
            {
                PermissionName = "View Account",
                PermissionCode = "ACCOUNT_VIEW",
                IsActive = true
            };

            _service.Create(dto);

            _repositoryMock.Verify(
                x => x.Create(It.Is<Permission>(p =>
                    p.CreatedOn != default(DateTime) &&
                    p.UpdatedOn == null)),
                Times.Once);
        }
    }
}
