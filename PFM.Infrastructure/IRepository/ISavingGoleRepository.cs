using PFM.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Infrastructure.IRepository
{
    public interface ISavingGoleRepository
    {
        void Create(SavingGole savingGole);
        void Update(SavingGole savingGole);
        void Delete(int Id);
        List<SavingGole> GetAll();
        SavingGole GetById(int Id);
    }
}
