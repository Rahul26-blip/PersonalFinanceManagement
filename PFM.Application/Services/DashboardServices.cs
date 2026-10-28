
using PFM.Application.DTO;
using PFM.Application.IServices;
using PFM.Infrastructure.IRepository;

namespace PFM.Application.Services
{
    public class DashboardServices : IDashboardServices
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IBudgetRepository _budgetRepository;

        public DashboardServices(
            IAccountRepository accountRepository,
            ITransactionRepository transactionRepository,
            IBudgetRepository budgetRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
            _budgetRepository = budgetRepository;
        }

        public DashboardDTO GetDashboard(int userId)
        {
            // Get the records belonging to this user only
            var accounts = _accountRepository.GetAll().Where(a => a.UserId == userId).ToList();

            var transactions = _transactionRepository.GetAll().Where(t => t.UserId == userId).ToList();

            var budgets = _budgetRepository.GetAll(userId);

            // Current month date range
            DateTime today = DateTime.Today;

            DateTime monthStart = new DateTime(today.Year, today.Month, 1);

            DateTime nextMonthStart = monthStart.AddMonths(1);

            // Total balance across the user's accounts
            decimal totalBalance = accounts.Sum(a => (decimal)a.CurrentBalance);

            // Current month's income
            decimal monthlyIncome = transactions.Where(t =>t.TransactionType.Equals("Income",StringComparison.OrdinalIgnoreCase)
                    && t.TransactionDate >= monthStart&& t.TransactionDate < nextMonthStart).Sum(t => (decimal)t.Amount);

            // Current month's expenses
            decimal monthlyExpenses = transactions.Where(t =>t.TransactionType.Equals( "Expense", StringComparison.OrdinalIgnoreCase)
                    && t.TransactionDate >= monthStart && t.TransactionDate < nextMonthStart).Sum(t => (decimal)t.Amount);

            // Calculate remaining budget for budgets active today.
            // Each budget uses its category and its own date range.
            DateOnly todayDate = DateOnly.FromDateTime(today);

            decimal remainingBudget = 0;

            var activeBudgets = budgets.Where(b => b.StartDate <= todayDate && b.EndDate >= todayDate).ToList();

            foreach (var budget in activeBudgets)
            {
                DateTime budgetStart = budget.StartDate.ToDateTime(TimeOnly.MinValue);

                DateTime budgetEndExclusive = budget.EndDate .AddDays(1) .ToDateTime(TimeOnly.MinValue);

                decimal categoryExpenses = transactions
                    .Where(t =>
                        t.TransactionType.Equals(
                            "Expense",
                            StringComparison.OrdinalIgnoreCase)
                        && t.CategoriesId == budget.CategoriesId
                        && t.TransactionDate >= budgetStart
                        && t.TransactionDate < budgetEndExclusive)
                    .Sum(t => (decimal)t.Amount);

                remainingBudget +=
                    (decimal)budget.Amount - categoryExpenses;
            }

            // Most recent five transactions
            var recentTransactions = transactions
                .OrderByDescending(t => t.TransactionDate)
                .ThenByDescending(t => t.TransactionId)
                .Take(5)
                .Select(t => new DashboardTransactionDTO
                {
                    TransactionId = t.TransactionId,
                    Description = t.Description,
                    TransactionType = t.TransactionType,
                    Amount = t.Amount,
                    TransactionDate = t.TransactionDate
                })
                .ToList();

            // Prepare the last six calendar months, including this month
            var monthlySummary = new List<DashboardMonthlyDTO>();

            for (int i = 5; i >= 0; i--)
            {
                DateTime start = monthStart.AddMonths(-i);
                DateTime end = start.AddMonths(1);

                decimal income = transactions
                    .Where(t =>
                        t.TransactionType.Equals(
                            "Income",
                            StringComparison.OrdinalIgnoreCase)
                        && t.TransactionDate >= start
                        && t.TransactionDate < end)
                    .Sum(t => (decimal)t.Amount);

                decimal expenses = transactions
                    .Where(t =>
                        t.TransactionType.Equals(
                            "Expense",
                            StringComparison.OrdinalIgnoreCase)
                        && t.TransactionDate >= start
                        && t.TransactionDate < end)
                    .Sum(t => (decimal)t.Amount);

                monthlySummary.Add(new DashboardMonthlyDTO
                {
                    Month = start.ToString("MMM"),
                    Income = income,
                    Expenses = expenses
                });
            }

            // Return all dashboard data
            return new DashboardDTO
            {
                TotalBalance = totalBalance,
                MonthlyIncome = monthlyIncome,
                MonthlyExpenses = monthlyExpenses,
                RemainingBudget = remainingBudget,
                RecentTransactions = recentTransactions,
                MonthlySummary = monthlySummary
            };
        }
    }
}
