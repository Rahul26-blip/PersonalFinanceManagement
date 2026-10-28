using Moq;
using PFM.Application.DTO;
using PFM.Application.Services;
using PFM.Domain.Entites;
using PFM.Infrastructure.IRepository;

namespace PFM.XTests
{
    public class AccountServicesTests
    {
        // TEST 1: GetAll returns accounts
        [Fact]
        public void GetAll_WhenAccountsExist_ReturnsAccounts()
        {
            // Arrange
            var repositoryMock = new Mock<IAccountRepository>();

            var accounts = new List<Account>
            {
                new Account
                {
                    AccountId = 1,
                    UserId = 11,
                    AccountName = "SBI",
                    AccountType = "Savings",
                    CurrentBalance = 20000,
                    IsActive = true
                }
            };

            repositoryMock.Setup(repo => repo.GetAll()).Returns(accounts);

            var service = new AccountServices(repositoryMock.Object);

            // Act
            var result = service.GetAll();

            // Assert
            Assert.Single(result);
            Assert.Equal(1, result[0].AccountId);
            Assert.Equal("SBI", result[0].AccountName);

            repositoryMock.Verify( repo => repo.GetAll(), Times.Once);
        }

        // TEST 2: GetAll returns empty list
        [Fact]
        public void GetAll_WhenNoAccountsExist_ReturnsEmptyList()
        {
            // Arrange
            var repositoryMock = new Mock<IAccountRepository>();

            repositoryMock.Setup(repo => repo.GetAll()).Returns(new List<Account>());

            var service = new AccountServices(repositoryMock.Object);

            // Act
            var result = service.GetAll();

            // Assert
            Assert.Empty(result);

            repositoryMock.Verify(repo => repo.GetAll(),Times.Once);
        }

        // TEST 3: GetAll maps account properties correctly
        [Fact]
        public void GetAll_WhenCalled_MapsAccountPropertiesCorrectly()
        {
            // Arrange
            var repositoryMock = new Mock<IAccountRepository>();

            var accounts = new List<Account>
            {
                new Account
                {
                    AccountId = 5,
                    UserId = 11,
                    AccountName = "HDFC",
                    AccountType = "Current",
                    CurrentBalance = 50000,
                    IsActive = true
                }
            };

            repositoryMock.Setup(repo => repo.GetAll()).Returns(accounts);

            var service = new AccountServices(repositoryMock.Object);

            // Act
            var result = service.GetAll();

            // Assert
            Assert.Single(result);
            Assert.Equal(5, result[0].AccountId);
            Assert.Equal(11, result[0].UserId);
            Assert.Equal("HDFC", result[0].AccountName);
            Assert.Equal("Current", result[0].AccountType);
            Assert.Equal(50000, result[0].CurrentBalance);
            Assert.True(result[0].IsActive);
        }

        // TEST 4: GetById returns an existing account
        [Fact]
        public void GetById_WhenAccountExists_ReturnsAccount()
        {
            // Arrange
            var repositoryMock = new Mock<IAccountRepository>();

            var account = new Account
            {
                AccountId = 5,
                UserId = 11,
                AccountName = "SBI",
                AccountType = "Savings",
                CurrentBalance = 20000,
                IsActive = true
            };

            repositoryMock.Setup(repo => repo.GetById(5)).Returns(account);

            var service = new AccountServices(repositoryMock.Object);

            // Act
            var result = service.GetById(5);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(5, result.AccountId);
            Assert.Equal(11, result.UserId);
            Assert.Equal("SBI", result.AccountName);
            Assert.Equal(20000, result.CurrentBalance);
        }

        // TEST 5: GetById passes correct ID
        [Fact]
        public void GetById_WhenCalled_PassesCorrectIdToRepository()
        {
            // Arrange
            var repositoryMock = new Mock<IAccountRepository>();

            repositoryMock.Setup(repo => repo.GetById(10))
                .Returns(new Account
                {
                    AccountId = 10,
                    UserId = 11,
                    AccountName = "Axis",
                    AccountType = "Savings"
                });

            var service = new AccountServices(repositoryMock.Object);

            // Act
            service.GetById(10);

            // Assert
            repositoryMock.Verify( repo => repo.GetById(10), Times.Once);
        }

        // TEST 6: GetById currently throws when account is missing
        [Fact]
        public void GetById_WhenAccountDoesNotExist_ThrowsNullReferenceException()
        {
            // Arrange
            var repositoryMock = new Mock<IAccountRepository>();

            repositoryMock.Setup(repo => repo.GetById(99)).Returns((Account)null!);

            var service = new AccountServices(repositoryMock.Object);

            // Act and Assert
            Assert.Throws<NullReferenceException>(() => service.GetById(99));
        }

        // TEST 7: Create passes correct account data
        [Fact]
        public void Create_WhenCalled_CreatesAccountInRepository()
        {
            // Arrange
            var repositoryMock = new Mock<IAccountRepository>();

            Account? createdAccount = null;

            repositoryMock.Setup(repo => repo.Create(It.IsAny<Account>())).Callback<Account>
                (account => createdAccount = account);

            var service = new AccountServices(repositoryMock.Object);

            var accountDTO = new AccountDTO
            {
                UserId = 11,
                AccountName = "SBI",
                AccountType = "Savings",
                CurrentBalance = 20000
            };

            // Act
            service.Create(accountDTO);

            // Assert
            Assert.NotNull(createdAccount);
            Assert.Equal(11, createdAccount.UserId);
            Assert.Equal("SBI", createdAccount.AccountName);
            Assert.Equal("Savings", createdAccount.AccountType);
            Assert.Equal(20000, createdAccount.CurrentBalance);

            repositoryMock.Verify(repo => repo.Create(It.IsAny<Account>()),Times.Once);
        }

        // TEST 8: Create sets account active
        [Fact]
        public void Create_WhenCalled_SetsIsActiveTrue()
        {
            // Arrange
            var repositoryMock = new Mock<IAccountRepository>();

            Account? createdAccount = null;

            repositoryMock.Setup(repo => repo.Create(It.IsAny<Account>())).Callback<Account>(
                    account => createdAccount = account);

            var service = new AccountServices(repositoryMock.Object);

            var accountDTO = new AccountDTO
            {
                UserId = 11,
                AccountName = "HDFC",
                AccountType = "Current",
                CurrentBalance = 10000,
                IsActive = false
            };

            // Act
            service.Create(accountDTO);

            // Assert
            Assert.NotNull(createdAccount);
            Assert.True(createdAccount.IsActive);
        }

        // TEST 9: Create assigns audit dates
        [Fact]
        public void Create_WhenCalled_AssignsAuditDates()
        {
            // Arrange
            var repositoryMock = new Mock<IAccountRepository>();

            Account? createdAccount = null;

            repositoryMock.Setup(repo => repo.Create(It.IsAny<Account>())).Callback<Account>(
                    account => createdAccount = account);

            var service = new AccountServices(repositoryMock.Object);

            var accountDTO = new AccountDTO
            {
                UserId = 11,
                AccountName = "ICICI",
                AccountType = "Savings",
                CurrentBalance = 15000
            };

            var before = DateTime.Now;

            // Act
            service.Create(accountDTO);

            var after = DateTime.Now;

            // Assert
            Assert.NotNull(createdAccount);

            Assert.InRange(createdAccount.CreatedOn,before,after);

            Assert.NotNull(createdAccount.UpdatedOn);

            Assert.InRange(createdAccount.UpdatedOn.Value,before,after);
        }

        // TEST 10: Update existing account
        [Fact]
        public void Update_WhenAccountExists_UpdatesAccountInRepository()
        {
            // Arrange
            var repositoryMock = new Mock<IAccountRepository>();

            var existingAccount = new Account
            {
                AccountId = 5,
                UserId = 11,
                AccountName = "Old Bank",
                AccountType = "Savings",
                CurrentBalance = 10000,
                IsActive = true
            };

            repositoryMock.Setup(repo => repo.GetById(5)).Returns(existingAccount);

            Account? updatedAccount = null;

            repositoryMock.Setup(repo => repo.Update(It.IsAny<Account>())).Callback<Account>(
                    account => updatedAccount = account);

            var service = new AccountServices(repositoryMock.Object);

            var accountDTO = new AccountDTO
            {
                AccountId = 5,
                UserId = 11,
                AccountName = "New Bank",
                AccountType = "Current",
                CurrentBalance = 30000,
                IsActive = true
            };

            // Act
            service.Update(accountDTO);

            // Assert
            Assert.NotNull(updatedAccount);
            Assert.Equal(5, updatedAccount.AccountId);
            Assert.Equal(11, updatedAccount.UserId);
            Assert.Equal("New Bank", updatedAccount.AccountName);
            Assert.Equal("Current", updatedAccount.AccountType);
            Assert.Equal(30000, updatedAccount.CurrentBalance);
            Assert.True(updatedAccount.IsActive);

            repositoryMock.Verify(repo => repo.GetById(5),Times.Once);

            repositoryMock.Verify(repo => repo.Update(It.IsAny<Account>()),Times.Once);
        }

        // TEST 11: Update currently throws when account is missing
        [Fact]
        public void Update_WhenAccountDoesNotExist_ThrowsNullReferenceException()
        {
            // Arrange
            var repositoryMock = new Mock<IAccountRepository>();

            repositoryMock.Setup(repo => repo.GetById(99)).Returns((Account)null!);

            var service = new AccountServices(repositoryMock.Object);

            var accountDTO = new AccountDTO
            {
                AccountId = 99,
                UserId = 11,
                AccountName = "SBI",
                AccountType = "Savings",
                CurrentBalance = 20000
            };

            // Act and Assert
            Assert.Throws<NullReferenceException>(() => service.Update(accountDTO));

            repositoryMock.Verify(repo => repo.Update(It.IsAny<Account>()),Times.Never);
        }

        // TEST 12: Delete passes correct account ID
        [Fact]
        public void Delete_WhenCalled_DeletesAccountFromRepository()
        {
            // Arrange
            var repositoryMock = new Mock<IAccountRepository>();

            var service = new AccountServices(repositoryMock.Object);

            int accountId = 5;

            // Act
            service.Delete(accountId);

            // Assert
            repositoryMock.Verify(repo => repo.Delete(5),Times.Once);
        }
    }
}
