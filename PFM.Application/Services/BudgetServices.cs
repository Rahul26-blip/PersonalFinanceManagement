using Microsoft.EntityFrameworkCore.ChangeTracking;
using PFM.Application.DTO;
using PFM.Application.IServices;
using PFM.Domain.Entites;
using PFM.Infrastructure.IRepository;

namespace PFM.Application.Services
{
    public class BudgetServices : IBudgetServices
    {
        private readonly IBudgetRepository _budgetRepository;
        public BudgetServices(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public void  Create(BudgetDTO budgetDTO)
        {
            Budget budget = new Budget();
            
            budget.UserId = budgetDTO.UserId;
            budget.CategoriesId = budgetDTO.CategoriesId;
            budget.Amount = budgetDTO.Amount;
            budget.StartDate = budgetDTO.StartDate;
            budget.EndDate = budgetDTO.EndDate;
            budget.IsActive = budgetDTO.IsActive;
            budget.CreatedOn=DateTime.Now;
            budget.UpdatedOn=DateTime.Now;

            _budgetRepository.Create(budget);
        }
        public void Update(BudgetDTO budgetDTO,int userId)
        {
            var budget = _budgetRepository.GetById(budgetDTO.BudgetId,userId);

            if (budget == null)
            {
                throw new KeyNotFoundException("Budget not found.");
            }

            budget.UserId = budgetDTO.UserId;
            budget.CategoriesId = budgetDTO.CategoriesId;
            budget.Amount = budgetDTO.Amount;
            budget.StartDate = budgetDTO.StartDate;
            budget.EndDate = budgetDTO.EndDate;
            budget.IsActive = budgetDTO.IsActive;
            budget.UpdatedOn = DateTime.Now;

            _budgetRepository.Update(budget);
        }
        public List<BudgetDTO> GetAll(int userId)
        {
            List<Budget> budgets = _budgetRepository.GetAll(userId);

            return budgets.Select(budget=>new BudgetDTO
            {
               BudgetId = budget.BudgetId,
               Amount = budget.Amount,
               CategoriesId=budget.CategoriesId,
               UserId=budget.UserId,
               StartDate=budget.StartDate,
               EndDate=budget.EndDate,
               CreatedOn=DateTime.Now,
               UpdatedOn=DateTime.Now,
               IsActive=budget.IsActive,

            }).ToList();
        }

        public BudgetDTO GetById(int Id,int userId)
        {
            var budget = _budgetRepository.GetById(Id,userId);

            if (budget == null)
            {
                return null;
            }

            return new BudgetDTO
            {
                BudgetId = budget.BudgetId,
                UserId = budget.UserId,
                Amount = budget.Amount,
                CategoriesId = budget.CategoriesId,
                IsActive = budget.IsActive,
                CreatedOn = budget.CreatedOn,
                UpdatedOn = budget.UpdatedOn,
                StartDate = budget.StartDate,
                EndDate = budget.EndDate,
            };
        }
        public void Delete(int Id)
        { 
            _budgetRepository.Delete(Id);
            
        }

    }
}
