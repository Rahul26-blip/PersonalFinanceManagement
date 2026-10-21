using PFM.Domain.Entites;
using PFM.Infrastructure.Context;
using PFM.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Infrastructure.Repository
{
    public  class InvestmentRepository: IInvestmentRepository
    {
        private readonly AppDbContext _Context;
        public InvestmentRepository(AppDbContext context) 
        {
            _Context = context; 
        }
        public List<Investment> GetAll()
        {
            return _Context.Investments.ToList();
        }
        public Investment GetById(int Id)
        {
            return _Context.Investments.FirstOrDefault(x => x.InvestmentId == Id);
        }
        public void Create(Investment investment)
        {
            investment.CreatedOn = DateTime.Now;
            investment.UpdatedOn = null;
            _Context.Investments.Add(investment);
            _Context.SaveChanges();
        }
        public void Update(Investment investment)
        {
            _Context.Investments.Update(investment);
            _Context.SaveChanges();
        }
        public void Delete(int Id)
        {
            var investment = _Context.Investments.FirstOrDefault(x=>x.InvestmentId==Id);

            _Context.Investments.Remove(investment);
            _Context.SaveChanges();
        }
    }

}
