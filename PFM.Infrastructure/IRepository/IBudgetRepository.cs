
using PFM.Domain.Entites;

namespace PFM.Infrastructure.IRepository
{
    public interface IBudgetRepository
    {
        List<Budget> GetAll();
        Budget GetById(int Id);
        void Update(Budget budget);
        void Delete(int Id);
        void Create(Budget budget);

    }
}
