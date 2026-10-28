
using Moq;
using PFM.Application.DTO;
using PFM.Application.Services;
using PFM.Domain.Entites;
using PFM.Infrastructure.IRepository;

namespace PFM.Tests
{
    public class RoleServicesTests
    {
        private readonly Mock<IRoleRepository> _roleRepositoryMock;
        private readonly RoleServices _service;

        public RoleServicesTests()
        {
            _roleRepositoryMock = new Mock<IRoleRepository>();

            _service = new RoleServices(
                _roleRepositoryMock.Object);
        }

        // Test 1: GetAll returns roles.
        [Fact]
        public void GetAll_WhenRolesExist_ShouldReturnRoles()
        {
            var roles = new List<Role>
            {
                new Role
                {
                    RoleId = 1,
                    RoleName = "Admin",
                    RoleCode = "ADMIN",
                    IsActive = true
                },
                new Role
                {
                    RoleId = 2,
                    RoleName = "User",
                    RoleCode = "USER",
                    IsActive = true
                }
            };

            _roleRepositoryMock
                .Setup(x => x.GetAll())
                .Returns(roles);

            var result = _service.GetAll();

            Assert.Equal(2, result.Count);
            Assert.Equal("Admin", result[0].RoleName);
            Assert.Equal("USER", result[1].RoleCode);
        }

        // Test 2: GetAll returns an empty list.
        [Fact]
        public void GetAll_WhenNoRolesExist_ShouldReturnEmptyList()
        {
            _roleRepositoryMock
                .Setup(x => x.GetAll())
                .Returns(new List<Role>());

            var result = _service.GetAll();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        // Test 3: GetById returns the matching role.
        [Fact]
        public void GetById_WhenRoleExists_ShouldReturnRoleDTO()
        {
            var role = new Role
            {
                RoleId = 1,
                RoleName = "Admin",
                RoleCode = "ADMIN",
                IsActive = true
            };

            _roleRepositoryMock
                .Setup(x => x.GetById(1))
                .Returns(role);

            var result = _service.GetById(1);

            Assert.NotNull(result);
            Assert.Equal(1, result.RoleId);
            Assert.Equal("Admin", result.RoleName);
            Assert.Equal("ADMIN", result.RoleCode);
            Assert.True(result.IsActive);
        }

        // Test 4: GetById returns null for a missing role.
        [Fact]
        public void GetById_WhenRoleDoesNotExist_ShouldReturnNull()
        {
            _roleRepositoryMock
                .Setup(x => x.GetById(99))
                .Returns((Role)null);

            var result = _service.GetById(99);

            Assert.Null(result);
        }

        // Test 5: Create maps the DTO to a Role entity.
        [Fact]
        public void Create_ShouldMapDTOAndCallRepository()
        {
            var dto = new RoleDTO
            {
                RoleName = "Admin",
                RoleCode = "ADMIN",
                IsActive = true
            };

            _service.Create(dto);

            _roleRepositoryMock.Verify(
                x => x.Create(It.Is<Role>(r =>
                    r.RoleName == "Admin" &&
                    r.RoleCode == "ADMIN" &&
                    r.IsActive)),
                Times.Once);
        }

        // Test 6: Update changes the existing role.
        [Fact]
        public void Update_WhenRoleExists_ShouldUpdateExistingRole()
        {
            var existingRole = new Role
            {
                RoleId = 5,
                RoleName = "OldName",
                RoleCode = "OLD",
                IsActive = true
            };

            var dto = new RoleDTO
            {
                RoleId = 5,
                RoleName = "NewName",
                RoleCode = "NEW",
                IsActive = false
            };

            _roleRepositoryMock
                .Setup(x => x.GetById(5))
                .Returns(existingRole);

            _service.Update(dto);

            Assert.Equal("NewName", existingRole.RoleName);
            Assert.Equal("NEW", existingRole.RoleCode);
            Assert.False(existingRole.IsActive);
            Assert.NotNull(existingRole.UpdatedOn);

            _roleRepositoryMock.Verify(
                x => x.Update(It.IsAny<Role>()),
                Times.Once);
        }

        // Test 7: Update handles a missing role.
        [Fact]
        public void Update_WhenRoleDoesNotExist_ShouldThrowException()
        {
            var dto = new RoleDTO
            {
                RoleId = 99,
                RoleName = "Admin",
                RoleCode = "ADMIN",
                IsActive = true
            };

            _roleRepositoryMock
                .Setup(x => x.GetById(99))
                .Returns((Role)null);

            Assert.Throws<KeyNotFoundException>(
                () => _service.Update(dto));

            _roleRepositoryMock.Verify(
                x => x.Update(It.IsAny<Role>()),
                Times.Never);
        }

        // Test 8: Delete calls the repository.
        [Fact]
        public void Delete_ShouldCallRepository()
        {
            _service.Delete(5);

            _roleRepositoryMock.Verify(
                x => x.Delete(5),
                Times.Once);
        }

        // Test 9: GetAll maps all role properties.
        [Fact]
        public void GetAll_ShouldMapRoleProperties()
        {
            var roles = new List<Role>
            {
                new Role
                {
                    RoleId = 10,
                    RoleName = "Manager",
                    RoleCode = "MGR",
                    IsActive = false
                }
            };

            _roleRepositoryMock
                .Setup(x => x.GetAll())
                .Returns(roles);

            var result = _service.GetAll();

            Assert.Single(result);
            Assert.Equal(10, result[0].RoleId);
            Assert.Equal("Manager", result[0].RoleName);
            Assert.Equal("MGR", result[0].RoleCode);
            Assert.False(result[0].IsActive);
        }

        // Test 10: Create sets the creation timestamp.
        [Fact]
        public void Create_ShouldSetCreatedOn()
        {
            var dto = new RoleDTO
            {
                RoleName = "User",
                RoleCode = "USER",
                IsActive = true
            };

            _service.Create(dto);

            _roleRepositoryMock.Verify(
                x => x.Create(It.Is<Role>(r =>
                    r.CreatedOn != default(DateTime) &&
                    r.UpdatedOn == null)),
                Times.Once);
        }
    }
}
