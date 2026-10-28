
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
    public class TransactionServicesTests
    {
        private readonly Mock<ITransactionRepository> _mockRepository;
        private readonly TransactionServices _transactionServices;

        public TransactionServicesTests()
        {
            // Create a fake repository for unit testing.
            _mockRepository = new Mock<ITransactionRepository>();

            // Pass the fake repository to the service.
            _transactionServices =
                new TransactionServices(_mockRepository.Object);
        }

        // TEST 1: GetAll returns transactions.
        [Fact]
        public void GetAll_WhenTransactionsExist_ReturnsTransactions()
        {
            // Arrange
            var transactions = new List<Transaction>
            {
                new Transaction
                {
                    TransactionId = 1,
                    UserId = 10,
                    AccountId = 2,
                    CategoriesId = 3,
                    TransactionType = "Expense",
                    Amount = 500,
                    TransactionDate = new DateTime(2026, 1, 10),
                    Description = "Food",
                    IsActive = true,
                    CreatedOn = new DateTime(2026, 1, 10)
                },
                new Transaction
                {
                    TransactionId = 2,
                    UserId = 10,
                    AccountId = 2,
                    CategoriesId = 4,
                    TransactionType = "Income",
                    Amount = 2000,
                    TransactionDate = new DateTime(2026, 1, 11),
                    Description = "Salary",
                    IsActive = true,
                    CreatedOn = new DateTime(2026, 1, 11)
                }
            };

            _mockRepository
                .Setup(r => r.GetAll())
                .Returns(transactions);

            // Act
            var result = _transactionServices.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Food", result[0].Description);
            Assert.Equal("Salary", result[1].Description);

            _mockRepository.Verify(
                r => r.GetAll(), Times.Once);
        }

        // TEST 2: GetAll returns an empty list.
        [Fact]
        public void GetAll_WhenNoTransactionsExist_ReturnsEmptyList()
        {
            // Arrange
            _mockRepository
                .Setup(r => r.GetAll())
                .Returns(new List<Transaction>());

            // Act
            var result = _transactionServices.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        // TEST 3: GetAll maps transaction properties correctly.
        [Fact]
        public void GetAll_MapsTransactionPropertiesCorrectly()
        {
            // Arrange
            var transactionDate = new DateTime(2026, 2, 10);
            var createdOn = new DateTime(2026, 2, 11);
            var updatedOn = new DateTime(2026, 2, 12);

            var transactions = new List<Transaction>
            {
                new Transaction
                {
                    TransactionId = 5,
                    UserId = 20,
                    AccountId = 3,
                    CategoriesId = 4,
                    TransactionType = "Expense",
                    Amount = 750,
                    TransactionDate = transactionDate,
                    Description = "Shopping",
                    IsActive = true,
                    CreatedOn = createdOn,
                    UpdatedOn = updatedOn
                }
            };

            _mockRepository
                .Setup(r => r.GetAll())
                .Returns(transactions);

            // Act
            var result = _transactionServices.GetAll();

            // Assert
            var item = Assert.Single(result);

            Assert.Equal(5, item.TransactionId);
            Assert.Equal(20, item.UserId);
            Assert.Equal(3, item.AccountId);
            Assert.Equal(4, item.CategoriesId);
            Assert.Equal("Expense", item.TransactionType);
            Assert.Equal(750, item.Amount);
            Assert.Equal(transactionDate, item.TransactionDate);
            Assert.Equal("Shopping", item.Description);
            Assert.True(item.IsActive);
            Assert.Equal(createdOn, item.CreatedOn);
            Assert.Equal(updatedOn, item.UpdatedOn);
        }

        // TEST 4: GetById returns an existing transaction.
        [Fact]
        public void GetById_WhenTransactionExists_ReturnsTransaction()
        {
            // Arrange
            var transaction = new Transaction
            {
                TransactionId = 1,
                UserId = 10,
                AccountId = 2,
                CategoriesId = 3,
                TransactionType = "Expense",
                Amount = 500,
                TransactionDate = new DateTime(2026, 3, 1),
                Description = "Groceries",
                IsActive = true
            };

            _mockRepository
                .Setup(r => r.GetById(1))
                .Returns(transaction);

            // Act
            var result = _transactionServices.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.TransactionId);
            Assert.Equal("Groceries", result.Description);
            Assert.Equal(500, result.Amount);
        }

        // TEST 5: GetById currently throws if the transaction is missing.
        [Fact]
        public void GetById_WhenTransactionDoesNotExist_ThrowsNullReferenceException()
        {
            // Arrange
            _mockRepository
                .Setup(r => r.GetById(99))
                .Returns((Transaction)null);

            // Act and Assert
            Assert.Throws<NullReferenceException>(
                () => _transactionServices.GetById(99));
        }

        // TEST 6: Create saves the correct transaction data.
        [Fact]
        public void Create_WhenTransactionDateIsValid_CreatesTransaction()
        {
            // Arrange
            var dto = new TransactionDTO
            {
                UserId = 10,
                AccountId = 2,
                CategoriesId = 3,
                TransactionType = "Expense",
                Amount = 500,
                TransactionDate = new DateTime(2026, 4, 15, 14, 30, 0),
                Description = "Lunch",
                IsActive = true
            };

            Transaction savedTransaction = null;

            _mockRepository
                .Setup(r => r.Create(It.IsAny<Transaction>()))
                .Callback<Transaction>(
                    t => savedTransaction = t);

            // Act
            _transactionServices.Create(dto);

            // Assert
            Assert.NotNull(savedTransaction);
            Assert.Equal(10, savedTransaction.UserId);
            Assert.Equal(2, savedTransaction.AccountId);
            Assert.Equal(3, savedTransaction.CategoriesId);
            Assert.Equal("Expense", savedTransaction.TransactionType);
            Assert.Equal(500, savedTransaction.Amount);
            Assert.Equal(
                new DateTime(2026, 4, 15),
                savedTransaction.TransactionDate);
            Assert.Equal("Lunch", savedTransaction.Description);
            Assert.True(savedTransaction.IsActive);
            Assert.Null(savedTransaction.UpdatedOn);

            _mockRepository.Verify(
                r => r.Create(It.IsAny<Transaction>()),
                Times.Once);
        }

        // TEST 7: Create rejects a missing transaction date.
        [Fact]
        public void Create_WhenTransactionDateIsDefault_ThrowsArgumentException()
        {
            // Arrange
            var dto = new TransactionDTO
            {
                UserId = 10,
                AccountId = 2,
                CategoriesId = 3,
                TransactionType = "Expense",
                Amount = 500,
                TransactionDate = default,
                Description = "Lunch",
                IsActive = true
            };

            // Act and Assert
            Assert.Throws<ArgumentException>(
                () => _transactionServices.Create(dto));

            _mockRepository.Verify(
                r => r.Create(It.IsAny<Transaction>()),
                Times.Never);
        }

        // TEST 8: Create rejects dates before SQL Server datetime range.
        [Fact]
        public void Create_WhenTransactionDateIsBeforeSqlDateRange_ThrowsArgumentException()
        {
            // Arrange
            var dto = new TransactionDTO
            {
                UserId = 10,
                AccountId = 2,
                CategoriesId = 3,
                TransactionType = "Expense",
                Amount = 500,
                TransactionDate = new DateTime(1700, 1, 1),
                Description = "Lunch",
                IsActive = true
            };

            // Act and Assert
            Assert.Throws<ArgumentException>(
                () => _transactionServices.Create(dto));

            _mockRepository.Verify(
                r => r.Create(It.IsAny<Transaction>()),
                Times.Never);
        }

        // TEST 9: Update changes an existing transaction.
        [Fact]
        public void Update_WhenTransactionExists_UpdatesTransaction()
        {
            // Arrange
            var existingTransaction = new Transaction
            {
                TransactionId = 1,
                UserId = 10,
                AccountId = 2,
                CategoriesId = 3,
                TransactionType = "Expense",
                Amount = 100,
                TransactionDate = new DateTime(2026, 1, 1),
                Description = "Old description",
                IsActive = true
            };

            var dto = new TransactionDTO
            {
                TransactionId = 1,
                UserId = 10,
                AccountId = 4,
                CategoriesId = 5,
                TransactionType = "Income",
                Amount = 2000,
                TransactionDate = new DateTime(2026, 4, 20),
                Description = "Updated description",
                IsActive = false
            };

            _mockRepository
                .Setup(r => r.GetById(1))
                .Returns(existingTransaction);

            // Act
            _transactionServices.Update(dto);

            // Assert
            Assert.Equal(10, existingTransaction.UserId);
            Assert.Equal(4, existingTransaction.AccountId);
            Assert.Equal(5, existingTransaction.CategoriesId);
            Assert.Equal("Income", existingTransaction.TransactionType);
            Assert.Equal(2000, existingTransaction.Amount);
            Assert.Equal(
                new DateTime(2026, 4, 20),
                existingTransaction.TransactionDate);
            Assert.Equal(
                "Updated description",
                existingTransaction.Description);
            Assert.False(existingTransaction.IsActive);
            Assert.NotNull(existingTransaction.UpdatedOn);

            _mockRepository.Verify(
                r => r.Update(existingTransaction),
                Times.Once);
        }

        // TEST 10: Update currently throws if the transaction is missing.
        [Fact]
        public void Update_WhenTransactionDoesNotExist_ThrowsNullReferenceException()
        {
            // Arrange
            var dto = new TransactionDTO
            {
                TransactionId = 99,
                UserId = 10,
                AccountId = 2,
                CategoriesId = 3,
                TransactionType = "Expense",
                Amount = 500,
                TransactionDate = new DateTime(2026, 4, 20),
                Description = "Missing transaction",
                IsActive = true
            };

            _mockRepository
                .Setup(r => r.GetById(99))
                .Returns((Transaction)null);

            // Act and Assert
            Assert.Throws<NullReferenceException>(
                () => _transactionServices.Update(dto));

            _mockRepository.Verify(
                r => r.Update(It.IsAny<Transaction>()),
                Times.Never);
        }

        // TEST 11: Delete passes the correct ID to the repository.
        [Fact]
        public void Delete_WhenCalled_PassesCorrectIdToRepository()
        {
            // Arrange
            int transactionId = 5;

            // Act
            _transactionServices.Delete(transactionId);

            // Assert
            _mockRepository.Verify(
                r => r.Delete(transactionId),
                Times.Once);
        }

        // TEST 12: Delete calls the repository only once.
        [Fact]
        public void Delete_WhenCalled_CallsRepositoryOnce()
        {
            // Arrange
            int transactionId = 10;

            // Act
            _transactionServices.Delete(transactionId);

            // Assert
            _mockRepository.Verify(
                r => r.Delete(It.IsAny<int>()),
                Times.Once);
        }
    }
}
