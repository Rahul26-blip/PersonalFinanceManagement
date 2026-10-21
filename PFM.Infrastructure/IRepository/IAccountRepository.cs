
using PFM.Domain.Entites;

namespace PFM.Infrastructure.IRepository
{
    public interface IAccountRepository
    {
        List<Account> GetAll();
        Account GetById(int id);
        void Create(Account account);
        void Update(Account account);
        void Delete(int id);
    }
}
