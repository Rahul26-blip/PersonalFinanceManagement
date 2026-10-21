using PFM.Domain.Entites;

namespace PFM.Infrastructure.IRepository
{
    public interface IDebtRepository
    {
        List<Debts> GetAll();
        Debts GetById(int Id);
        void Create(Debts debts);
        void Update(Debts debts);
        void Delete(int  Id);

    }
}
