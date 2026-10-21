
namespace PFM.Application.DTO
{
    public class DashboardDTO
    {
        // Total of the logged-in user's account balances
        public decimal TotalBalance { get; set; }

        // Income for the current month
        public decimal MonthlyIncome { get; set; }

        // Expenses for the current month
        public decimal MonthlyExpenses { get; set; }

        // Current budget amount minus expenses in its period
        public decimal RemainingBudget { get; set; }

        // Latest transactions to display on the dashboard
        public List<DashboardTransactionDTO> RecentTransactions { get; set; }
            = new List<DashboardTransactionDTO>();

        // Monthly income and expense totals for the chart
        public List<DashboardMonthlyDTO> MonthlySummary { get; set; }
            = new List<DashboardMonthlyDTO>();
    }

    public class DashboardTransactionDTO
    {
        public int TransactionId { get; set; }
        public string Description { get; set; } = string.Empty;
        public string TransactionType { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }

    public class DashboardMonthlyDTO
    {
        public string Month { get; set; } = string.Empty;
        public decimal Income { get; set; }
        public decimal Expenses { get; set; }
    }
}
