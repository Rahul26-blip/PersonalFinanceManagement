using PFM.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Infrastructure.IRepository
{
    public interface ITransactionRepository
    {
        List<Transaction> GetAll();
        Transaction GetById(int Id);
        void Delete(int Id);
        void Create(Transaction transaction);
        void Update(Transaction transaction);
    }
}
