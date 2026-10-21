using PFM.Domain.Entites;
using PFM.Infrastructure.Context;
using PFM.Infrastructure.IRepository;

namespace PFM.Infrastructure.Repository
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly AppDbContext _context;
        public BudgetRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<Budget> GetAll()
        {
            return _context.Budgets.ToList();
        }
        public Budget GetById(int Id)
        {
            return _context.Budgets.FirstOrDefault(x => x.BudgetId == Id);
        }
        public void Create(Budget budget)
        {
            _context.Budgets.Add(budget);
            _context.SaveChanges();
        }
        public void Update(Budget budget)
        {
            _context.Budgets.Update(budget);
            _context.SaveChanges();
        }
        public void Delete(int Id)
        {
            var budget = _context.Budgets.FirstOrDefault(x => x.BudgetId == Id);

            _context.Budgets.Remove(budget);

            _context.SaveChanges();
        }
    }
}
