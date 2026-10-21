
using PFM.Application.DTO;
using PFM.Application.IServices;
using PFM.Domain.Entites;
using PFM.Infrastructure.IRepository;

namespace PFM.Application.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly IAccountRepository _accountRepository;
        public AccountServices (IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public void Create(AccountDTO accountDTO)
        {
            Account account = new Account();

            account.UserId = accountDTO.UserId;
            account.AccountName = accountDTO.AccountName;
            account.AccountType = accountDTO.AccountType;
            account.CurrentBalance = accountDTO.CurrentBalance;
            account.IsActive = true;
            account.CreatedOn = DateTime.Now;
            account.UpdatedOn = DateTime.Now;

            _accountRepository.Create(account);

        }

        public void Update(AccountDTO accountDTO)

        { 
            Account account =_accountRepository.GetById(accountDTO.AccountId);
            account.UserId = accountDTO.UserId;
            account.AccountName = accountDTO.AccountName;
            account.AccountType = accountDTO.AccountType;
            account.CurrentBalance = accountDTO.CurrentBalance;
            account.IsActive = accountDTO.IsActive;
            account.UpdatedOn = DateTime.Now;

            _accountRepository.Update(account);
        }

        public List<AccountDTO> GetAll()
        {
            List<Account> accounts = _accountRepository.GetAll();

            return  accounts.Select(account => new AccountDTO
            {
                AccountId = account.AccountId,
                AccountName = account.AccountName,
                AccountType = account.AccountType,
                UserId = account.UserId,
                CurrentBalance = account.CurrentBalance,
                IsActive = account.IsActive,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,

            }).ToList();
            
        }
        public AccountDTO GetById(int id)
        {
            var account = _accountRepository.GetById(id);

            return new AccountDTO
            {
                UserId = account.UserId,
                AccountId = account.AccountId,
                AccountName = account.AccountName,
                AccountType = account.AccountType,
                CurrentBalance = account.CurrentBalance,
                IsActive = account.IsActive,
                CreatedOn = DateTime.Now,
                UpdatedOn = DateTime.Now,

            };
        }
        public void Delete(int  Id)
        {
            _accountRepository.Delete(Id);
        }
    }
}
