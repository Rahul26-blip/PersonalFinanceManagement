using PFM.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Application.IServices
{
    public interface IInvestmentServices
    {

        List<InvestmentDTO> GetAll();
        InvestmentDTO GetById(int Id);
        void Update(InvestmentDTO investmentDTO);
        void Delete(int Id);
        void Create(InvestmentDTO investmentDTO);

    }

}
