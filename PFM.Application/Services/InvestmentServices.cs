using PFM.Application.DTO;
using PFM.Application.IServices;
using PFM.Domain.Entites;
using PFM.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Application.Services
{
    public class InvestmentServices: IInvestmentServices
    {
        private readonly IInvestmentRepository _repository;
        public InvestmentServices(IInvestmentRepository repository)
        {
            _repository = repository;
        }
        public List<InvestmentDTO> GetAll()
        {
            List<Investment> investments = _repository.GetAll();
            return investments.Select(investment => new InvestmentDTO
            {
                InvestmentId = investment.InvestmentId,
                UserId = investment.UserId,
                InvestmentName = investment.InvestmentName,
                InvestmentType = investment.InvestmentType,
                InvestmentDate = investment.InvestmentDate,
                InvestedAmount = investment.InvestedAmount,
                CurrentValue = investment.CurrentValue,
                IsActive = investment.IsActive,
                CreatedOn = investment.CreatedOn,
                UpdatedOn = investment.UpdatedOn,

            }).ToList();
        }
       
        public InvestmentDTO GetById(int Id)
        {
            var investment = _repository.GetById(Id);

            return new InvestmentDTO
            {
                InvestmentId = investment.InvestmentId,
                UserId = investment.UserId,
                InvestmentName = investment.InvestmentName,
                InvestmentType = investment.InvestmentType,
                InvestmentDate = investment.InvestmentDate,
                InvestedAmount = investment.InvestedAmount,
                CurrentValue = investment.CurrentValue,
                IsActive = investment.IsActive,
                CreatedOn = investment.CreatedOn,
                UpdatedOn = investment.UpdatedOn,
            };
        }
        public void Create(InvestmentDTO investmentDTO)
        {
            Investment investment = new Investment();

            investment.UserId = investmentDTO.UserId;
            investment.InvestmentName = investmentDTO.InvestmentName;
            investment.InvestmentType = investmentDTO.InvestmentType;
            investment.InvestmentDate = investmentDTO.InvestmentDate;
            investment.InvestedAmount = investmentDTO.InvestedAmount;
            investment.CurrentValue = investmentDTO.CurrentValue;
            investment.IsActive = investmentDTO.IsActive;
            investment.UpdatedOn = investmentDTO.UpdatedOn;
            _repository.Create(investment);
        }
        public void Update(InvestmentDTO investmentDTO)
        {
            var investment = _repository.GetById(investmentDTO.InvestmentId);
            investment.InvestmentId = investmentDTO.InvestmentId;
            investment.UserId = investmentDTO.UserId;
            investment.InvestmentName = investmentDTO.InvestmentName;
            investment.InvestmentType = investmentDTO.InvestmentType;
            investment.InvestmentDate = investmentDTO.InvestmentDate;
            investment.InvestedAmount = investmentDTO.InvestedAmount;
            investment.CurrentValue = investmentDTO.CurrentValue;
            investment.IsActive = investmentDTO.IsActive;
            investment.CreatedOn = investmentDTO.CreatedOn;
            investment.UpdatedOn = DateTime.Now;
            _repository.Update(investment);

        }
        public void Delete(int Id)
        {
            _repository.Delete(Id);
        }

    }
}
