
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
    public class InvestmentServicesTests
    {
        private readonly Mock<IInvestmentRepository> _mockRepository;
        private readonly InvestmentServices _investmentServices;

        public InvestmentServicesTests()
        {
            // Create a fake repository for unit testing.
            _mockRepository = new Mock<IInvestmentRepository>();

            // Inject the fake repository into the service.
            _investmentServices =
                new InvestmentServices(_mockRepository.Object);
        }

        // TEST 1: GetAll returns investments.
        [Fact]
        public void GetAll_WhenInvestmentsExist_ReturnsInvestments()
        {
            // Arrange
            var investments = new List<Investment>
            {
                new Investment
                {
                    InvestmentId = 1,
                    UserId = 10,
                    InvestmentName = "Mutual Fund",
                    InvestmentType = "Mutual Fund",
                    InvestedAmount = 10000,
                    CurrentValue = 12000,
                    InvestmentDate = new DateOnly(2026, 1, 10),
                    IsActive = true,
                    CreatedOn = new DateTime(2026, 1, 10)
                },
                new Investment
                {
                    InvestmentId = 2,
                    UserId = 10,
                    InvestmentName = "Gold",
                    InvestmentType = "Gold",
                    InvestedAmount = 20000,
                    CurrentValue = 25000,
                    InvestmentDate = new DateOnly(2026, 2, 10),
                    IsActive = true,
                    CreatedOn = new DateTime(2026, 2, 10)
                }
            };

            _mockRepository
                .Setup(r => r.GetAll())
                .Returns(investments);

            // Act
            var result = _investmentServices.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Mutual Fund", result[0].InvestmentName);
            Assert.Equal("Gold", result[1].InvestmentName);

            _mockRepository.Verify(
                r => r.GetAll(), Times.Once);
        }

        // TEST 2: GetAll returns an empty list.
        [Fact]
        public void GetAll_WhenNoInvestmentsExist_ReturnsEmptyList()
        {
            // Arrange
            _mockRepository
                .Setup(r => r.GetAll())
                .Returns(new List<Investment>());

            // Act
            var result = _investmentServices.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        // TEST 3: GetAll maps investment properties correctly.
        [Fact]
        public void GetAll_MapsInvestmentPropertiesCorrectly()
        {
            // Arrange
            var investmentDate = new DateOnly(2026, 3, 15);
            var createdOn = new DateTime(2026, 3, 16);
            var updatedOn = new DateTime(2026, 3, 20);

            var investments = new List<Investment>
            {
                new Investment
                {
                    InvestmentId = 5,
                    UserId = 20,
                    InvestmentName = "Fixed Deposit",
                    InvestmentType = "Bank Deposit",
                    InvestedAmount = 50000,
                    CurrentValue = 55000,
                    InvestmentDate = investmentDate,
                    IsActive = true,
                    CreatedOn = createdOn,
                    UpdatedOn = updatedOn
                }
            };

            _mockRepository
                .Setup(r => r.GetAll())
                .Returns(investments);

            // Act
            var result = _investmentServices.GetAll();

            // Assert
            var item = Assert.Single(result);

            Assert.Equal(5, item.InvestmentId);
            Assert.Equal(20, item.UserId);
            Assert.Equal("Fixed Deposit", item.InvestmentName);
            Assert.Equal("Bank Deposit", item.InvestmentType);
            Assert.Equal(50000, item.InvestedAmount);
            Assert.Equal(55000, item.CurrentValue);
            Assert.Equal(investmentDate, item.InvestmentDate);
            Assert.True(item.IsActive);
            Assert.Equal(createdOn, item.CreatedOn);
            Assert.Equal(updatedOn, item.UpdatedOn);
        }

        // TEST 4: GetById returns an existing investment.
        [Fact]
        public void GetById_WhenInvestmentExists_ReturnsInvestment()
        {
            // Arrange
            var investment = new Investment
            {
                InvestmentId = 1,
                UserId = 10,
                InvestmentName = "Mutual Fund",
                InvestmentType = "Mutual Fund",
                InvestedAmount = 10000,
                CurrentValue = 12000,
                InvestmentDate = new DateOnly(2026, 1, 10),
                IsActive = true
            };

            _mockRepository
                .Setup(r => r.GetById(1))
                .Returns(investment);

            // Act
            var result = _investmentServices.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.InvestmentId);
            Assert.Equal("Mutual Fund", result.InvestmentName);
            Assert.Equal(10000, result.InvestedAmount);
            Assert.Equal(12000, result.CurrentValue);

            _mockRepository.Verify(
                r => r.GetById(1), Times.Once);
        }

        // TEST 5: Current service throws if investment is missing.
        [Fact]
        public void GetById_WhenInvestmentDoesNotExist_ShouldReturnNull()
        {
            _mockRepository.Setup(x => x.GetById(99))
                .Returns((Investment)null);

            var result = _investmentServices.GetById(99);

            Assert.Null(result);
        }

        // TEST 6: Create passes the correct data to the repository.
        [Fact]
        public void Create_WhenCalled_CreatesInvestment()
        {
            // Arrange
            var dto = new InvestmentDTO
            {
                UserId = 10,
                InvestmentName = "Mutual Fund",
                InvestmentType = "Mutual Fund",
                InvestedAmount = 10000,
                CurrentValue = 12000,
                InvestmentDate = new DateOnly(2026, 4, 15),
                IsActive = true,
                UpdatedOn = null
            };

            Investment savedInvestment = null;

            _mockRepository
                .Setup(r => r.Create(It.IsAny<Investment>()))
                .Callback<Investment>(
                    i => savedInvestment = i);

            // Act
            _investmentServices.Create(dto);

            // Assert
            Assert.NotNull(savedInvestment);
            Assert.Equal(10, savedInvestment.UserId);
            Assert.Equal("Mutual Fund", savedInvestment.InvestmentName);
            Assert.Equal("Mutual Fund", savedInvestment.InvestmentType);
            Assert.Equal(10000, savedInvestment.InvestedAmount);
            Assert.Equal(12000, savedInvestment.CurrentValue);
            Assert.Equal(
                new DateOnly(2026, 4, 15),
                savedInvestment.InvestmentDate);
            Assert.True(savedInvestment.IsActive);
            Assert.Null(savedInvestment.UpdatedOn);

            _mockRepository.Verify(
                r => r.Create(It.IsAny<Investment>()),
                Times.Once);
        }

        // TEST 7: Update changes an existing investment.
        [Fact]
        public void Update_WhenInvestmentExists_UpdatesInvestment()
        {
            // Arrange
            var existingInvestment = new Investment
            {
                InvestmentId = 1,
                UserId = 10,
                InvestmentName = "Old Investment",
                InvestmentType = "Shares",
                InvestedAmount = 10000,
                CurrentValue = 11000,
                InvestmentDate = new DateOnly(2026, 1, 1),
                IsActive = true,
                CreatedOn = new DateTime(2026, 1, 1)
            };

            var dto = new InvestmentDTO
            {
                InvestmentId = 1,
                UserId = 10,
                InvestmentName = "Updated Investment",
                InvestmentType = "Mutual Fund",
                InvestedAmount = 20000,
                CurrentValue = 23000,
                InvestmentDate = new DateOnly(2026, 4, 1),
                IsActive = true,
                CreatedOn = existingInvestment.CreatedOn
            };

            _mockRepository
                .Setup(r => r.GetById(1))
                .Returns(existingInvestment);

            // Act
            _investmentServices.Update(dto);

            // Assert
            Assert.Equal(
                "Updated Investment",
                existingInvestment.InvestmentName);
            Assert.Equal(
                "Mutual Fund",
                existingInvestment.InvestmentType);
            Assert.Equal(20000, existingInvestment.InvestedAmount);
            Assert.Equal(23000, existingInvestment.CurrentValue);
            Assert.Equal(
                new DateOnly(2026, 4, 1),
                existingInvestment.InvestmentDate);
            Assert.True(existingInvestment.IsActive);
            Assert.Equal(
                dto.CreatedOn,
                existingInvestment.CreatedOn);
            Assert.NotNull(existingInvestment.UpdatedOn);

            _mockRepository.Verify(
                r => r.Update(existingInvestment),
                Times.Once);
        }

        // TEST 8: Current service throws if investment is missing.
        [Fact]
        public void Update_WhenInvestmentDoesNotExist_ThrowsNullReferenceException()
        {
            // Arrange
            var dto = new InvestmentDTO
            {
                InvestmentId = 99,
                UserId = 10,
                InvestmentName = "Missing Investment",
                InvestmentType = "Shares",
                InvestedAmount = 10000,
                CurrentValue = 10000,
                InvestmentDate = new DateOnly(2026, 4, 1),
                IsActive = true
            };

            _mockRepository
                .Setup(r => r.GetById(99))
                .Returns((Investment)null);

            // Act and Assert
            Assert.Throws<NullReferenceException>(
                () => _investmentServices.Update(dto));

            _mockRepository.Verify(
                r => r.Update(It.IsAny<Investment>()),
                Times.Never);
        }

        // TEST 9: Delete passes the correct ID to the repository.
        [Fact]
        public void Delete_WhenCalled_PassesCorrectIdToRepository()
        {
            // Arrange
            int investmentId = 5;

            // Act
            _investmentServices.Delete(investmentId);

            // Assert
            _mockRepository.Verify(
                r => r.Delete(investmentId),
                Times.Once);
        }

        // TEST 10: Delete calls the repository only once.
        [Fact]
        public void Delete_WhenCalled_CallsRepositoryOnce()
        {
            // Arrange
            int investmentId = 10;

            // Act
            _investmentServices.Delete(investmentId);

            // Assert
            _mockRepository.Verify(
                r => r.Delete(It.IsAny<int>()),
                Times.Once);
        }
    }
}
