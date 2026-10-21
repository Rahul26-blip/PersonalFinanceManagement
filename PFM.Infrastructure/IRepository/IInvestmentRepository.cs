using PFM.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Infrastructure.IRepository
{
    public  interface IInvestmentRepository
    {
        List<Investment> GetAll();
        Investment GetById(int Id);
        void Create(Investment investment);
        void Update(Investment investment);
        void Delete(int Id);
    }
    
}
