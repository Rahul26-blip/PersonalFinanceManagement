using Moq;
using PFM.Application.DTO;
using PFM.Application.Services;
using PFM.Domain.Entites;
using PFM.Infrastructure.IRepository;

namespace PFM.XTests
{
    public class BudgetServicesTests
    {
        // TEST 1: GetAll returns the user's budgets
        [Fact]
        public void GetAll_WhenCalledWithUserId_ReturnsUserBudgets()
        {
            // Arrange
            var repositoryMock = new Mock<IBudgetRepository>();

            var budgets = new List<Budget>
            {
                new Budget
                {
                    BudgetId = 5,
                    UserId = 11,
                    CategoriesId = 5,
                    Amount = 20000,
                    StartDate = new DateOnly(2026, 10, 1),
                    EndDate = new DateOnly(2026, 10, 31)
                }
            };

            repositoryMock.Setup(repo => repo.GetAll(11)).Returns(budgets);

            var service = new BudgetServices(repositoryMock.Object);

            // Act
            var result = service.GetAll(11);

            // Assert
            Assert.Single(result);
            Assert.Equal(5, result[0].BudgetId);
            Assert.Equal(11, result[0].UserId);
            Assert.Equal(20000, result[0].Amount);

            repositoryMock.Verify(repo => repo.GetAll(11),Times.Once);
        }

        // TEST 2: GetAll returns an empty list when no budgets exist
        [Fact]
        public void GetAll_WhenNoBudgetsExist_ReturnsEmptyList()
        {
            // Arrange
            var repositoryMock = new Mock<IBudgetRepository>();

            repositoryMock.Setup(repo => repo.GetAll(99)).Returns(new List<Budget>());

            var service = new BudgetServices(repositoryMock.Object);

            // Act
            var result = service.GetAll(99);

            // Assert
            Assert.Empty(result);
        }

        // TEST 3: GetById returns the requested budget
        [Fact]
        public void GetById_WhenBudgetExists_ReturnsBudget()
        {
            // Arrange
            var repositoryMock = new Mock<IBudgetRepository>();

            var mockBudget = new Budget
            {
                BudgetId = 5,
                UserId = 11,
                CategoriesId = 5,
                Amount = 20000,
                StartDate = new DateOnly(2026, 10, 1),
                EndDate = new DateOnly(2026, 10, 31)
            };

            repositoryMock.Setup(repo => repo.GetById(5, 11)).Returns(mockBudget);

            var service = new BudgetServices(repositoryMock.Object);

            // Act
            var result = service.GetById(5, 11);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.BudgetId);
            Assert.Equal(11, result.UserId);
            Assert.Equal(20000, result.Amount);

            repositoryMock.Verify( repo => repo.GetById(5, 11), Times.Once);
        }

        // TEST 4: GetById returns null when the budget is not found
        [Fact]
        public void GetById_WhenBudgetDoesNotExist_ReturnsNull()
        {
            // Arrange
            var repositoryMock = new Mock<IBudgetRepository>();

            repositoryMock.Setup(repo => repo.GetById(5, 11)).Returns((Budget?)null);

            var service = new BudgetServices(repositoryMock.Object);

            // Act
            var result = service.GetById(5, 11);

            // Assert
            Assert.Null(result);
        }
        [Fact]
        public void Create_WhenCalled_CreatesBudgetInRepository()
        {
            var repositoryMock = new Mock<IBudgetRepository>();

            Budget? createdBudget = null;

            repositoryMock.Setup(repo => repo.Create(It.IsAny<Budget>()))
                .Callback<Budget>(buget=>createdBudget = buget);

            var services = new BudgetServices(repositoryMock.Object);

            var budgetDTO = new BudgetDTO
            {
                UserId = 11,
                CategoriesId = 5,
                Amount = 20000,
                StartDate = new DateOnly(2026, 10, 1),
                EndDate = new DateOnly(2026, 10, 31),
                IsActive = true
            };

            // Act: Call the service Create method
            services.Create(budgetDTO);


            // Assert: Verify the repository received the budget
            Assert.NotNull(createdBudget);
            Assert.Equal(11, createdBudget.UserId);
            Assert.Equal(5, createdBudget.CategoriesId);
            Assert.Equal(20000, createdBudget.Amount);
            Assert.Equal(new DateOnly(2026, 10, 1), createdBudget.StartDate);
            Assert.Equal(new DateOnly(2026, 10, 31), createdBudget.EndDate);
            Assert.True(createdBudget.IsActive);


            // Check that audit dates were assigned
            Assert.NotEqual(default(DateTime), createdBudget.CreatedOn);
            Assert.NotEqual(default(DateTime), createdBudget.UpdatedOn);

            repositoryMock.Verify(repo=>repo.Create(It.IsAny<Budget>()),Times.Once());


        }

        [Fact]
        public void Update_whilebugetExists_UpdateBudgetInResponce()
        {
            var repositoryMock = new Mock<IBudgetRepository>();

            var Exitingbudget = new Budget
            {
                BudgetId = 5,
                UserId = 11,
                CategoriesId = 3,
                Amount = 10000,
                StartDate = new DateOnly(2026, 9, 1),
                EndDate = new DateOnly(2026, 9, 30),
                IsActive = true
            };

            repositoryMock.Setup(repo => repo.GetById(5, 11)).Returns(Exitingbudget);

            // Capture the budget passed to Update()
            Budget? updatedBudget = null;

            repositoryMock.Setup(repo=>repo.Update(It.IsAny<Budget>()))
                .Callback<Budget>(budget => updatedBudget = budget);

            var service = new BudgetServices(repositoryMock.Object);

            // New values to update
            var budgetDTO = new BudgetDTO
            {
                BudgetId = 5,
                UserId = 11,
                CategoriesId = 5,
                Amount = 20000,
                StartDate = new DateOnly(2026, 10, 1),
                EndDate = new DateOnly(2026, 10, 31),
                IsActive = true
            };

            // Act: Call Update
            service.Update(budgetDTO, 11);

            // Assert: Check updated values
            Assert.NotNull(updatedBudget);

            Assert.Equal(5, updatedBudget.CategoriesId);
            Assert.Equal(20000, updatedBudget.Amount);
            Assert.Equal(new DateOnly(2026, 10, 1), updatedBudget.StartDate);
            Assert.Equal(new DateOnly(2026, 10, 31), updatedBudget.EndDate);

            // Verify repository Update was called once
            repositoryMock.Verify(repo => repo.Update(It.IsAny<Budget>()),Times.Once);

        }

        [Fact]
        public void Update_WhenBudgetDoesNotExist_ThrowsKeyNotFoundException()
        {
            // Arrange
            var repositoryMock = new Mock<IBudgetRepository>();

            repositoryMock.Setup(repo => repo.GetById(5, 11)).Returns((Budget)null!);

            var service = new BudgetServices(repositoryMock.Object);

            var budgetDTO = new BudgetDTO
            {
                BudgetId = 5,
                UserId = 11,
                CategoriesId = 5,
                Amount = 20000,
                StartDate = new DateOnly(2026, 10, 1),
                EndDate = new DateOnly(2026, 10, 31),
                IsActive = true
            };

            // Act and Assert
            Assert.Throws<KeyNotFoundException>(() => service.Update(budgetDTO, 11));

            // Repository Update should not be called
            repositoryMock.Verify(repo => repo.Update(It.IsAny<Budget>()),Times.Never);
        }

        [Fact]
        public void Delete_WhenCalled_DeletesBudgetFromRepository()
        {
            // Arrange
            var repositoryMock = new Mock<IBudgetRepository>();

            var service = new BudgetServices(repositoryMock.Object);

            int budgetId = 5;

            // Act
            service.Delete(budgetId);

            // Assert
            repositoryMock.Verify(repo => repo.Delete(5),Times.Once);
        }

        [Fact]
        public void Delete_PassesCorrectBudgetIdToRepository()
        {
            // Arrange
            var repositoryMock = new Mock<IBudgetRepository>();

            var service = new BudgetServices(repositoryMock.Object);

            int budgetId = 10;

            // Act
            service.Delete(budgetId);

            // Assert
            repositoryMock.Verify(repo => repo.Delete(budgetId),Times.Once);
        }


    }
}
