using PFM.Application.DTO;
using PFM.Application.IServices;
using PFM.Domain.Entites;
using PFM.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Application.Services
{
    public class DebtServices : IDebtServices
    {
        private readonly IDebtRepository _debtRepository;
        public DebtServices(IDebtRepository debtRepository)
        {
            _debtRepository = debtRepository;
        }
        public List<DebtsDTO> GatAll()
        {
            List<Debts> debts = _debtRepository.GetAll();

            return debts.Select(debt => new DebtsDTO
            {
                DebtsId = debt.DebtsId,
                DueDate = debt.DueDate,
                DebtsName = debt.DebtsName,
                InterestRate = debt.InterestRate,
                UserId = debt.UserId,
                RemainingAmount = debt.RemainingAmount,
                ToltalAmount = debt.ToltalAmount,
                IsPaid = debt.IsPaid,
                IsActive = debt.IsActive,
                CreatedOn = debt.CreatedOn,
                UpdatedOn   = debt.UpdatedOn,
            }).ToList();
        }
        public DebtsDTO GetById(int id)
        {
            Debts debts = _debtRepository.GetById(id);
            return new DebtsDTO
            {
                DebtsId = debts.DebtsId,
                DebtsName = debts.DebtsName,
                DueDate = debts.DueDate,
                InterestRate = debts.InterestRate,
                IsPaid = debts.IsPaid,
                UpdatedOn = debts.UpdatedOn,
                UserId = debts.UserId,
                RemainingAmount = debts.RemainingAmount,
                ToltalAmount = debts.ToltalAmount,
                IsActive = debts.IsActive,
                CreatedOn = debts.CreatedOn,
            };
        }
        
        public void Create(DebtsDTO debtsDTO)
        {
            Debts debts = new Debts();

            debts.DebtsName = debtsDTO.DebtsName;
            debts.UserId = debtsDTO.UserId;
            debts.DueDate = debtsDTO.DueDate;
            debts.IsPaid = debtsDTO.IsPaid; 
            debts.ToltalAmount  = debtsDTO.ToltalAmount;
            debts.RemainingAmount = debtsDTO.RemainingAmount;
            debts.InterestRate = debtsDTO.InterestRate;
            debts.IsActive = debtsDTO.IsActive;
            debts.CreatedOn = DateTime.Now;
            debts.UpdatedOn = null;

            _debtRepository.Create(debts);
        }
        public void Update(DebtsDTO debtsDTO)
        {
            Debts debts = _debtRepository.GetById(debtsDTO.DebtsId);

            debts.DebtsName = debtsDTO.DebtsName;
            debts.UserId = debtsDTO.UserId;
            debts.DueDate = debtsDTO.DueDate;
            debts.IsPaid = debtsDTO.IsPaid;
            debts.DebtsId = debtsDTO.DebtsId;
            debts.ToltalAmount = debtsDTO.ToltalAmount;
            debts.RemainingAmount = debtsDTO.RemainingAmount;
            debts.InterestRate = debtsDTO.InterestRate;
            debts.IsActive = debtsDTO.IsActive;
            debts.UpdatedOn = DateTime.Now;

            _debtRepository.Update(debts);
        }
        public void Delete(int Id)
        {

             _debtRepository.Delete(Id);
        }


    }
}
