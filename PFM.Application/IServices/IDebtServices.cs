using PFM.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Application.IServices
{
    public interface IDebtServices
    {
        List<DebtsDTO> GatAll();
        DebtsDTO GetById(int id);
        void Create(DebtsDTO debtsDTO);
        void Update(DebtsDTO debtsDTO);
        void Delete(int id);
    }
}
