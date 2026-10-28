using Moq;
using PFM.Application.DTO;
using PFM.Application.Services;
using PFM.Domain.Entites;
using PFM.Infrastructure.IRepository;

namespace PFM.Tests
{
    public class RolePermissionServicesTests
    {
        private readonly Mock<IRolePermissionRepository> _repositoryMock;
        private readonly RolePermissionServices _service;
        public RolePermissionServicesTests()
        {
            _repositoryMock = new Mock<IRolePermissionRepository>();
            _service = new RolePermissionServices(_repositoryMock.Object);
        }

        // Test 1: GetAll returns records mapped to DTOs
        [Fact]
        public void GetAll_WhenRecordsExist_ShouldReturnDTOList()
        {
            var records = new List<RolePermission>
            {
                new RolePermission
                {
                    RolePermissionId = 1,
                    RoleId = 2,
                    PermissionId = 3,
                    IsActive = true,
                    CreatedOn = new DateTime(2026, 1, 1)
                }
            };

            _repositoryMock.Setup(x => x.GetAll()).Returns(records);

            var result = _service.GetAll();

            Assert.Single(result);
            Assert.Equal(1, result[0].RolePermissionId);
            Assert.Equal(2, result[0].RoleId);
            Assert.Equal(3, result[0].PermissionId);
            Assert.True(result[0].IsActive);
        }

        // Test 2: GetAll returns an empty list when no records exist
        [Fact]
        public void GetAll_WhenNoRecordsExist_ShouldReturnEmptyList()
        {
            _repositoryMock.Setup(x => x.GetAll())
                .Returns(new List<RolePermission>());

            var result = _service.GetAll();

            Assert.NotNull(result);
            Assert.Empty(result);
        }

        // Test 3: GetById returns the matching DTO
        [Fact]
        public void GetById_WhenRecordExists_ShouldReturnDTO()
        {
            var record = new RolePermission
            {
                RolePermissionId = 5,
                RoleId = 2,
                PermissionId = 4,
                IsActive = true,
                CreatedOn = new DateTime(2026, 1, 1)
            };

            _repositoryMock.Setup(x => x.GetById(5)).Returns(record);

            var result = _service.GetById(5);

            Assert.NotNull(result);
            Assert.Equal(5, result.RolePermissionId);
            Assert.Equal(2, result.RoleId);
            Assert.Equal(4, result.PermissionId);
        }

        // Test 4: GetById returns null when the record is missing
        [Fact]
        public void GetById_WhenRecordDoesNotExist_ShouldReturnNull()
        {
            _repositoryMock.Setup(x => x.GetById(99))
                .Returns((RolePermission)null);

            var result = _service.GetById(99);

            Assert.Null(result);
        }

        // Test 5: Create sends the correct properties to the repository
        [Fact]
        public void Create_ShouldPassCorrectEntityToRepository()
        {
            var dto = new RolePermissionDTO
            {
                RoleId = 2,
                PermissionId = 7,
                IsActive = true
            };

            _service.Create(dto);

            _repositoryMock.Verify(x => x.Create(
                It.Is<RolePermission>(item =>
                    item.RoleId == 2 &&
                    item.PermissionId == 7 &&
                    item.IsActive == true)),
                Times.Once);
        }

        // Test 6: Create sets CreatedOn and leaves UpdatedOn null
        [Fact]
        public void Create_ShouldSetCreatedOnAndLeaveUpdatedOnNull()
        {
            var dto = new RolePermissionDTO
            {
                RoleId = 2,
                PermissionId = 7,
                IsActive = true
            };

            var beforeCreate = DateTime.Now;

            _service.Create(dto);

            var afterCreate = DateTime.Now;

            _repositoryMock.Verify(x => x.Create(
                It.Is<RolePermission>(item =>
                    item.CreatedOn >= beforeCreate &&
                    item.CreatedOn <= afterCreate &&
                    item.UpdatedOn == null)),
                Times.Once);
        }

        // Test 7: Update changes properties and saves the entity
        [Fact]
        public void Update_WhenRecordExists_ShouldUpdateRecord()
        {
            var existingRecord = new RolePermission
            {
                RolePermissionId = 10,
                RoleId = 1,
                PermissionId = 2,
                IsActive = false,
                CreatedOn = new DateTime(2026, 1, 1)
            };

            var dto = new RolePermissionDTO
            {
                RolePermissionId = 10,
                RoleId = 3,
                PermissionId = 4,
                IsActive = true
            };

            _repositoryMock.Setup(x => x.GetById(10))
                .Returns(existingRecord);

            _service.Update(dto);

            _repositoryMock.Verify(x => x.Update(
                It.Is<RolePermission>(item =>
                    item.RolePermissionId == 10 &&
                    item.RoleId == 3 &&
                    item.PermissionId == 4 &&
                    item.IsActive == true &&
                    item.UpdatedOn != null)),
                Times.Once);
        }

        // Test 8: Update throws an exception when the record is missing
        [Fact]
        public void Update_WhenRecordDoesNotExist_ShouldThrowException()
        {
            var dto = new RolePermissionDTO
            {
                RolePermissionId = 99,
                RoleId = 2,
                PermissionId = 3,
                IsActive = true
            };

            _repositoryMock.Setup(x => x.GetById(99))
                .Returns((RolePermission)null);

            Assert.Throws<KeyNotFoundException>(
                () => _service.Update(dto));

            _repositoryMock.Verify(
                x => x.Update(It.IsAny<RolePermission>()),
                Times.Never);
        }

        // Test 9: Delete calls the repository with the correct ID
        [Fact]
        public void Delete_ShouldCallRepositoryDelete()
        {
            _service.Delete(10);

            _repositoryMock.Verify(
                x => x.Delete(10),
                Times.Once);
        }

        // Test 10: GetAll maps UpdatedOn correctly
        [Fact]
        public void GetAll_ShouldMapUpdatedOnCorrectly()
        {
            var updatedDate = new DateTime(2026, 5, 10);

            var records = new List<RolePermission>
            {
                new RolePermission
                {
                    RolePermissionId = 1,
                    RoleId = 2,
                    PermissionId = 3,
                    IsActive = true,
                    CreatedOn = new DateTime(2026, 1, 1),
                    UpdatedOn = updatedDate
                }
            };

            _repositoryMock.Setup(x => x.GetAll()).Returns(records);

            var result = _service.GetAll();

            Assert.Equal(updatedDate, result[0].UpdatedOn);
        }
    }
}
