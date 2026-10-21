
namespace PFM.UI.Models
{
    public class DashboardModel
    {
        public decimal TotalBalance { get; set; }

        public decimal MonthlyIncome { get; set; }

        public decimal MonthlyExpenses { get; set; }

        public decimal RemainingBudget { get; set; }

        public List<DashboardTransactionModel> RecentTransactions
        { get; set; } = new List<DashboardTransactionModel>();

        public List<DashboardMonthlyModel> MonthlySummary
        { get; set; } = new List<DashboardMonthlyModel>();
    }

    public class DashboardTransactionModel
    {
        public int TransactionId { get; set; }

        public string Description { get; set; } = string.Empty;

        public string TransactionType { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public DateTime TransactionDate { get; set; }
    }

    public class DashboardMonthlyModel
    {
        public string Month { get; set; } = string.Empty;

        public decimal Income { get; set; }

        public decimal Expenses { get; set; }
    }
}
