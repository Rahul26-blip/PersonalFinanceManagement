using PFM.Application.DTO;

namespace PFM.Application.IServices
{
    public interface IBudgetServices
    {
        List<BudgetDTO> GetAll();
        BudgetDTO GetById(int id);
        void Update(BudgetDTO budgetDTO);
        void Delete(int id);
        void Create(BudgetDTO budgetDTO);
    }
}
