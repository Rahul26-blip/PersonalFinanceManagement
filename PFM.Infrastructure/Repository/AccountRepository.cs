using PFM.Domain.Entites;
using PFM.Infrastructure.Context;
using PFM.Infrastructure.IRepository;

namespace PFM.Infrastructure.Repository
{
    public class AccountRepository: IAccountRepository
    {
        private readonly AppDbContext _context;
        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Account> GetAll()
        {
            return _context.Accounts.ToList();
        }
        public Account GetById(int Id)
        {
            return _context.Accounts.FirstOrDefault(x=>x.AccountId == Id);
        }
        public void Create(Account account)
        {
            _context.Accounts.Add(account);
            _context.SaveChanges();
        }
        public void Update(Account account)
        {
            _context.Accounts.Update(account);
            _context.SaveChanges();
        }
        public void Delete(int Id)
        {
            var account = _context.Accounts.FirstOrDefault(x=>x.AccountId == Id);

            if (account == null)
            {
                return;
            }
            _context.Accounts.Remove(account);
            _context.SaveChanges();
            
        }

    }
}
