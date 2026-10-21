using PFM.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Application.IServices
{
    public interface ITransactionServices
    {
        List<TransactionDTO> GetAll();
        TransactionDTO GetById(int id);
        void Create(TransactionDTO transactionDTO);
        void Update(TransactionDTO transactionDTO);
        void Delete(int Id);
    }
}
