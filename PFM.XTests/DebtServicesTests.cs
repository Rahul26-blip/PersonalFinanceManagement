
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
    public class DebtServicesTests
    {
        private readonly Mock<IDebtRepository> _mockRepository;
        private readonly DebtServices _debtServices;

        public DebtServicesTests()
        {
            // Create a fake repository.
            _mockRepository = new Mock<IDebtRepository>();

            // Inject the fake repository into the service.
            _debtServices = new DebtServices(_mockRepository.Object);
        }

        // TEST 1: GatAll returns debts.
        [Fact]
        public void GatAll_WhenDebtsExist_ReturnsDebts()
        {
            // Arrange
            var debts = new List<Debts>
            {
                new Debts
                {
                    DebtsId = 1,
                    UserId = 10,
                    DebtsName = "Personal Loan",
                    ToltalAmount = 100000,
                    RemainingAmount = 50000,
                    InterestRate = 10,
                    DueDate = new DateOnly(2027, 1, 1),
                    IsPaid = false,
                    IsActive = true,
                    CreatedOn = new DateTime(2026, 1, 1)
                },
                new Debts
                {
                    DebtsId = 2,
                    UserId = 10,
                    DebtsName = "Car Loan",
                    ToltalAmount = 500000,
                    RemainingAmount = 250000,
                    InterestRate = 8,
                    DueDate = new DateOnly(2027, 6, 1),
                    IsPaid = false,
                    IsActive = true,
                    CreatedOn = new DateTime(2026, 1, 2)
                }
            };

            _mockRepository.Setup(r => r.GetAll()).Returns(debts);

            // Act
            var result = _debtServices.GatAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Personal Loan", result[0].DebtsName);
            Assert.Equal("Car Loan", result[1].DebtsName);

            _mockRepository.Verify(r => r.GetAll(), Times.Once);
        }

        // TEST 2: GatAll returns an empty list.
        [Fact]
        public void GatAll_WhenNoDebtsExist_ReturnsEmptyList()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetAll()).Returns(new List<Debts>());

            // Act
            var result = _debtServices.GatAll();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        // TEST 3: GatAll maps debt properties correctly.
        [Fact]
        public void GatAll_MapsDebtPropertiesCorrectly()
        {
            // Arrange
            var createdOn = new DateTime(2026, 2, 10);
            var updatedOn = new DateTime(2026, 2, 15);
            var dueDate = new DateOnly(2027, 3, 1);

            var debts = new List<Debts>
            {
                new Debts
                {
                    DebtsId = 5,
                    UserId = 20,
                    DebtsName = "Home Loan",
                    ToltalAmount = 1000000,
                    RemainingAmount = 700000,
                    InterestRate = 9,
                    DueDate = dueDate,
                    IsPaid = false,
                    IsActive = true,
                    CreatedOn = createdOn,
                    UpdatedOn = updatedOn
                }
            };

            _mockRepository.Setup(r => r.GetAll()).Returns(debts);

            // Act
            var result = _debtServices.GatAll();

            // Assert
            var item = Assert.Single(result);

            Assert.Equal(5, item.DebtsId);
            Assert.Equal(20, item.UserId);
            Assert.Equal("Home Loan", item.DebtsName);
            Assert.Equal(1000000, item.ToltalAmount);
            Assert.Equal(700000, item.RemainingAmount);
            Assert.Equal(9, item.InterestRate);
            Assert.Equal(dueDate, item.DueDate);
            Assert.False(item.IsPaid);
            Assert.True(item.IsActive);
            Assert.Equal(createdOn, item.CreatedOn);
            Assert.Equal(updatedOn, item.UpdatedOn);
        }

        // TEST 4: GetById returns an existing debt.
        [Fact]
        public void GetById_WhenDebtExists_ReturnsDebt()
        {
            // Arrange
            var debt = new Debts
            {
                DebtsId = 1,
                UserId = 10,
                DebtsName = "Personal Loan",
                ToltalAmount = 100000,
                RemainingAmount = 50000,
                InterestRate = 10,
                DueDate = new DateOnly(2027, 1, 1),
                IsPaid = false,
                IsActive = true
            };

            _mockRepository.Setup(r => r.GetById(1)).Returns(debt);

            // Act
            var result = _debtServices.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.DebtsId);
            Assert.Equal("Personal Loan", result.DebtsName);
            Assert.Equal(100000, result.ToltalAmount);
            Assert.Equal(50000, result.RemainingAmount);
        }

        // TEST 5: GetById currently throws if debt is missing.
        [Fact]
        public void GetById_WhenDebtDoesNotExist_ThrowsNullReferenceException()
        {
            // Arrange
            _mockRepository.Setup(r => r.GetById(99)).Returns((Debts)null);

            // Act and Assert
            Assert.Throws<NullReferenceException>(() => _debtServices.GetById(99));
        }

        // TEST 6: Create passes the correct data to the repository.
        [Fact]
        public void Create_WhenCalled_CreatesDebt()
        {
            // Arrange
            var dto = new DebtsDTO
            {
                UserId = 10,
                DebtsName = "Personal Loan",
                ToltalAmount = 100000,
                RemainingAmount = 50000,
                InterestRate = 10,
                DueDate = new DateOnly(2027, 1, 1),
                IsPaid = false,
                IsActive = true
            };

            Debts savedDebt = null;

            _mockRepository.Setup(r => r.Create(It.IsAny<Debts>())).Callback<Debts>(d => savedDebt = d);

            // Act
            _debtServices.Create(dto);

            // Assert
            Assert.NotNull(savedDebt);
            Assert.Equal(10, savedDebt.UserId);
            Assert.Equal("Personal Loan", savedDebt.DebtsName);
            Assert.Equal(100000, savedDebt.ToltalAmount);
            Assert.Equal(50000, savedDebt.RemainingAmount);
            Assert.Equal(10, savedDebt.InterestRate);
            Assert.Equal(new DateOnly(2027, 1, 1), savedDebt.DueDate);
            Assert.False(savedDebt.IsPaid);
            Assert.True(savedDebt.IsActive);
            Assert.True(savedDebt.CreatedOn <= DateTime.Now);
            Assert.Null(savedDebt.UpdatedOn);

            _mockRepository.Verify(r => r.Create(It.IsAny<Debts>()),Times.Once);
        }

        // TEST 7: Update modifies an existing debt.
        [Fact]
        public void Update_WhenDebtExists_UpdatesDebt()
        {
            // Arrange
            var existingDebt = new Debts
            {
                DebtsId = 1,
                UserId = 10,
                DebtsName = "Old Loan",
                ToltalAmount = 100000,
                RemainingAmount = 80000,
                InterestRate = 10,
                DueDate = new DateOnly(2027, 1, 1),
                IsPaid = false,
                IsActive = true,
                CreatedOn = new DateTime(2026, 1, 1)
            };

            var dto = new DebtsDTO
            {
                DebtsId = 1,
                UserId = 10,
                DebtsName = "Updated Loan",
                ToltalAmount = 150000,
                RemainingAmount = 60000,
                InterestRate = 8,
                DueDate = new DateOnly(2027, 6, 1),
                IsPaid = false,
                IsActive = true,
                CreatedOn = existingDebt.CreatedOn
            };

            _mockRepository.Setup(r => r.GetById(1)).Returns(existingDebt);

            // Act
            _debtServices.Update(dto);

            // Assert
            Assert.Equal("Updated Loan", existingDebt.DebtsName);
            Assert.Equal(150000, existingDebt.ToltalAmount);
            Assert.Equal(60000, existingDebt.RemainingAmount);
            Assert.Equal(8, existingDebt.InterestRate);
            Assert.Equal(new DateOnly(2027, 6, 1), existingDebt.DueDate);
            Assert.True(existingDebt.UpdatedOn.HasValue);

            _mockRepository.Verify(
                r => r.Update(existingDebt),
                Times.Once);
        }

        // TEST 8: Update currently throws if debt is missing.
        [Fact]
        public void Update_WhenDebtDoesNotExist_ThrowsNullReferenceException()
        {
            // Arrange
            var dto = new DebtsDTO
            {
                DebtsId = 99,
                UserId = 10,
                DebtsName = "Missing Loan",
                ToltalAmount = 100000,
                RemainingAmount = 50000,
                IsActive = true
            };

            _mockRepository.Setup(r => r.GetById(99)).Returns((Debts)null);

            // Act and Assert
            Assert.Throws<NullReferenceException>(() => _debtServices.Update(dto));

            _mockRepository.Verify( r => r.Update(It.IsAny<Debts>()), Times.Never);
        }

        // TEST 9: Delete passes the correct ID.
        [Fact]
        public void Delete_WhenCalled_PassesCorrectIdToRepository()
        {
            // Arrange
            int debtId = 5;

            // Act
            _debtServices.Delete(debtId);

            // Assert
            _mockRepository.Verify(r => r.Delete(debtId),Times.Once);
        }

        // TEST 10: Delete calls the repository only once.
        [Fact]
        public void Delete_WhenCalled_CallsRepositoryOnce()
        {
            // Arrange
            int debtId = 10;

            // Act
            _debtServices.Delete(debtId);

            // Assert
            _mockRepository.Verify(
                r => r.Delete(It.IsAny<int>()),
                Times.Once);
        }
    }
}
