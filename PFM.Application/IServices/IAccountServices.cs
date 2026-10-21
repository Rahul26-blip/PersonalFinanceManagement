using PFM.Application.DTO;

namespace PFM.Application.IServices
{
    public interface IAccountServices
    {
        List<AccountDTO> GetAll();
        AccountDTO GetById(int id);
        void Create(AccountDTO accountDTO);
        void Update(AccountDTO accountDTO);
        void Delete(int id);
    }
}
