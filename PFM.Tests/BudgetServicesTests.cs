using Moq;
using NUnit.Framework;
using PFM.Application.Services;
using PFM.Domain.Entites;
using PFM.Infrastructure.IRepository;

namespace PFM.Tests
{
    [TestFixture]
    public class BudgetServicesTests
    {
        private Mock<IBudgetRepository> _budgetRepositoryMock = null;
        private BudgetServices _budgetServices = null;

        [SetUp]
        public void SetUp()
        {
            _budgetRepositoryMock = new Mock<IBudgetRepository>();

            _budgetServices = new BudgetServices(_budgetRepositoryMock.Object);
        }
        [Test]
        public void GetAll_WhenCalledWithUserId_ReturnOnlyRepositoryResult()
        {
            var buget = new List<Budget>
            {
                new Budget
                {
                    BudgetId = 5,
                    UserId = 11,
                    CategoriesId = 5,
                    Amount = 20000,
                    StartDate = new DateOnly(2026, 10, 1),
                    EndDate = new DateOnly(2026, 10, 31),
                }
            };
            _budgetRepositoryMock.Setup(repo => repo.GetAll(11)).Returns(buget);

            var result = _budgetServices.GetAll(11);

            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0].BudgetId, Is.EqualTo(5));
            Assert.That(result[0].UserId, Is.EqualTo(11));
            Assert.That(result[0].Amount, Is.EqualTo(20000));

            _budgetRepositoryMock.Verify(repo => repo.GetAll(11), Times.Once);
        }

        [Test]
        public void GetAll_WhenRepositoryReturnsNoBudgets_ReturnsEmptyList()
        {
            _budgetRepositoryMock.Setup(repo => repo.GetAll(99)).Returns(new List<Budget>());

            var result = new List<Budget>();

            Assert.That(result, Is.Empty);
        }
    }
}
