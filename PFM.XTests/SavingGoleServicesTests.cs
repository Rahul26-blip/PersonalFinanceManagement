
using System;
using System.Collections.Generic;
using Moq;
using Xunit;

using PFM.Application.DTO;
using PFM.Application.Services;
using PFM.Domain.Entites;
using PFM.Infrastructure.IRepository;

namespace PFM.XTests
{
    public class SavingGoleServicesTests
    {
        private readonly Mock<ISavingGoleRepository> _mockRepository;
        private readonly SavingGoleServices _savingGoleServices;

        public SavingGoleServicesTests()
        {
            // Create a fake repository for unit testing.
            _mockRepository = new Mock<ISavingGoleRepository>();

            // Inject the fake repository into the service.
            _savingGoleServices =
                new SavingGoleServices(_mockRepository.Object);
        }

        // TEST 1: GetAll returns saving goals.
        [Fact]
        public void GetAll_WhenSavingGoalsExist_ReturnsSavingGoals()
        {
            // Arrange
            var goals = new List<SavingGole>
            {
                new SavingGole
                {
                    SavingsGolesId = 1,
                    UserId = 10,
                    GoleName = "New Car",
                    TargetAmount = 500000,
                    CurrentAmount = 100000,
                    TargetDate = new DateOnly(2027, 1, 1),
                    IsCompleted = false,
                    IsActive = true,
                    CreatedOn = new DateTime(2026, 1, 1)
                },
                new SavingGole
                {
                    SavingsGolesId = 2,
                    UserId = 10,
                    GoleName = "Vacation",
                    TargetAmount = 100000,
                    CurrentAmount = 25000,
                    TargetDate = new DateOnly(2027, 6, 1),
                    IsCompleted = false,
                    IsActive = true,
                    CreatedOn = new DateTime(2026, 1, 2)
                }
            };

            _mockRepository
                .Setup(r => r.GetAll())
                .Returns(goals);

            // Act
            var result = _savingGoleServices.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("New Car", result[0].GoleName);
            Assert.Equal("Vacation", result[1].GoleName);

            _mockRepository.Verify(
                r => r.GetAll(), Times.Once);
        }

        // TEST 2: GetAll returns an empty list.
        [Fact]
        public void GetAll_WhenNoSavingGoalsExist_ReturnsEmptyList()
        {
            // Arrange
            _mockRepository
                .Setup(r => r.GetAll())
                .Returns(new List<SavingGole>());

            // Act
            var result = _savingGoleServices.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        // TEST 3: GetAll maps all properties correctly.
        [Fact]
        public void GetAll_MapsSavingGoalPropertiesCorrectly()
        {
            // Arrange
            var createdOn = new DateTime(2026, 2, 10);
            var updatedOn = new DateTime(2026, 2, 15);
            var targetDate = new DateOnly(2027, 3, 1);

            var goals = new List<SavingGole>
            {
                new SavingGole
                {
                    SavingsGolesId = 5,
                    UserId = 20,
                    GoleName = "House",
                    TargetAmount = 1000000,
                    CurrentAmount = 300000,
                    TargetDate = targetDate,
                    IsCompleted = false,
                    IsActive = true,
                    CreatedOn = createdOn,
                    UpdatedOn = updatedOn
                }
            };

            _mockRepository
                .Setup(r => r.GetAll())
                .Returns(goals);

            // Act
            var result = _savingGoleServices.GetAll();

            // Assert
            var item = Assert.Single(result);

            Assert.Equal(5, item.SavingsGolesId);
            Assert.Equal(20, item.UserId);
            Assert.Equal("House", item.GoleName);
            Assert.Equal(1000000, item.TargetAmount);
            Assert.Equal(300000, item.CurrentAmount);
            Assert.Equal(targetDate, item.TargetDate);
            Assert.False(item.IsCompleted);
            Assert.True(item.IsActive);
            Assert.Equal(createdOn, item.CreatedOn);
            Assert.Equal(updatedOn, item.UpdatedOn);
        }

        // TEST 4: GetById returns an existing saving goal.
        [Fact]
        public void GetById_WhenSavingGoalExists_ReturnsSavingGoal()
        {
            // Arrange
            var goal = new SavingGole
            {
                SavingsGolesId = 1,
                UserId = 10,
                GoleName = "New Car",
                TargetAmount = 500000,
                CurrentAmount = 100000,
                TargetDate = new DateOnly(2027, 1, 1),
                IsCompleted = false,
                IsActive = true
            };

            _mockRepository
                .Setup(r => r.GetById(1))
                .Returns(goal);

            // Act
            var result = _savingGoleServices.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.SavingsGolesId);
            Assert.Equal("New Car", result.GoleName);
            Assert.Equal(500000, result.TargetAmount);
            Assert.Equal(100000, result.CurrentAmount);
        }

        // TEST 5: GetById currently throws if the goal is missing.
        [Fact]
        public void GetById_WhenSavingGoalDoesNotExist_ThrowsNullReferenceException()
        {
            // Arrange
            _mockRepository
                .Setup(r => r.GetById(99))
                .Returns((SavingGole)null);

            // Act and Assert
            Assert.Throws<NullReferenceException>(
                () => _savingGoleServices.GetById(99));
        }

        // TEST 6: Create passes correct data to the repository.
        [Fact]
        public void Create_WhenCalled_CreatesSavingGoal()
        {
            // Arrange
            var dto = new SavingGoleDTO
            {
                UserId = 10,
                GoleName = "New Car",
                TargetAmount = 500000,
                CurrentAmount = 100000,
                TargetDate = new DateOnly(2027, 1, 1),
                IsCompleted = false,
                IsActive = true,
                CreatedOn = new DateTime(2026, 2, 1),
                UpdatedOn = null
            };

            SavingGole savedGoal = null;

            _mockRepository
                .Setup(r => r.Create(It.IsAny<SavingGole>()))
                .Callback<SavingGole>(g => savedGoal = g);

            // Act
            _savingGoleServices.Create(dto);

            // Assert
            Assert.NotNull(savedGoal);
            Assert.Equal(10, savedGoal.UserId);
            Assert.Equal("New Car", savedGoal.GoleName);
            Assert.Equal(500000, savedGoal.TargetAmount);
            Assert.Equal(100000, savedGoal.CurrentAmount);
            Assert.Equal(new DateOnly(2027, 1, 1), savedGoal.TargetDate);
            Assert.False(savedGoal.IsCompleted);
            Assert.True(savedGoal.IsActive);
            Assert.Equal(dto.CreatedOn, savedGoal.CreatedOn);
            Assert.Null(savedGoal.UpdatedOn);

            _mockRepository.Verify(
                r => r.Create(It.IsAny<SavingGole>()),
                Times.Once);
        }

        // TEST 7: Update modifies an existing saving goal.
        [Fact]
        public void Update_WhenSavingGoalExists_UpdatesSavingGoal()
        {
            // Arrange
            var existingGoal = new SavingGole
            {
                SavingsGolesId = 1,
                UserId = 10,
                GoleName = "Old Goal",
                TargetAmount = 100000,
                CurrentAmount = 20000,
                TargetDate = new DateOnly(2027, 1, 1),
                IsCompleted = false,
                IsActive = true,
                CreatedOn = new DateTime(2026, 1, 1)
            };

            var dto = new SavingGoleDTO
            {
                SavingsGolesId = 1,
                UserId = 10,
                GoleName = "Updated Goal",
                TargetAmount = 200000,
                CurrentAmount = 50000,
                TargetDate = new DateOnly(2027, 6, 1),
                IsCompleted = false,
                IsActive = true,
                CreatedOn = existingGoal.CreatedOn
            };

            _mockRepository
                .Setup(r => r.GetById(1))
                .Returns(existingGoal);

            // Act
            _savingGoleServices.Update(dto);

            // Assert
            Assert.Equal("Updated Goal", existingGoal.GoleName);
            Assert.Equal(200000, existingGoal.TargetAmount);
            Assert.Equal(50000, existingGoal.CurrentAmount);
            Assert.Equal(new DateOnly(2027, 6, 1), existingGoal.TargetDate);
            Assert.Null(existingGoal.UpdatedOn);

            _mockRepository.Verify(
                r => r.Update(existingGoal),
                Times.Once);
        }

        // TEST 8: Update currently throws if the goal is missing.
        [Fact]
        public void Update_WhenSavingGoalDoesNotExist_ThrowsNullReferenceException()
        {
            // Arrange
            var dto = new SavingGoleDTO
            {
                SavingsGolesId = 99,
                UserId = 10,
                GoleName = "Missing Goal",
                TargetAmount = 100000,
                CurrentAmount = 0,
                TargetDate = new DateOnly(2027, 1, 1),
                IsCompleted = false,
                IsActive = true
            };

            _mockRepository
                .Setup(r => r.GetById(99))
                .Returns((SavingGole)null);

            // Act and Assert
            Assert.Throws<NullReferenceException>(
                () => _savingGoleServices.Update(dto));

            _mockRepository.Verify(
                r => r.Update(It.IsAny<SavingGole>()),
                Times.Never);
        }

        // TEST 9: Delete passes the correct ID to the repository.
        [Fact]
        public void Delete_WhenCalled_PassesCorrectIdToRepository()
        {
            // Arrange
            int goalId = 5;

            // Act
            _savingGoleServices.Delete(goalId);

            // Assert
            _mockRepository.Verify(
                r => r.Delete(goalId),
                Times.Once);
        }

        // TEST 10: Delete calls the repository only once.
        [Fact]
        public void Delete_WhenCalled_CallsRepositoryOnce()
        {
            // Arrange
            int goalId = 10;

            // Act
            _savingGoleServices.Delete(goalId);

            // Assert
            _mockRepository.Verify(
                r => r.Delete(It.IsAny<int>()),
                Times.Once);
        }
    }
}
