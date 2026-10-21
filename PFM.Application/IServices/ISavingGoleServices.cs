using PFM.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Application.IServices
{
    public interface ISavingGoleServices
    {
        void Create(SavingGoleDTO saveingGoleDTO);
        void Update(SavingGoleDTO saveingGoleDTO);
        void Delete(int Id);
        List<SavingGoleDTO> GetAll();
        SavingGoleDTO GetById(int id);
    }
}
