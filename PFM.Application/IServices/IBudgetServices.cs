using PFM.Application.DTO;

namespace PFM.Application.IServices
{
    public interface IBudgetServices
    {
        List<BudgetDTO> GetAll(int userId);
        BudgetDTO GetById(int id,int userId);
        void Update(BudgetDTO budgetDTO, int userId);
        void Delete(int id);
        void Create(BudgetDTO budgetDTO);
    }
}
