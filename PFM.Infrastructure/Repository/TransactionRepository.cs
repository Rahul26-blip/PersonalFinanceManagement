using PFM.Domain.Entites;
using PFM.Infrastructure.Context;
using PFM.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Infrastructure.Repository
{
    public class TransactionRepository: ITransactionRepository
    {
        private readonly AppDbContext _DbContext;
        public TransactionRepository(AppDbContext context)
        {
            _DbContext = context;
        }
        public List<Transaction> GetAll()
        {
            return _DbContext.Transactions.ToList();
        }
        public Transaction GetById(int Id)
        {
            return _DbContext.Transactions.FirstOrDefault(x => x.TransactionId == Id);
        }
        public void Create(Transaction transaction)
        {
            // Set the creation date
            transaction.CreatedOn = DateTime.Now;

            // No update date for a new transaction
            transaction.UpdatedOn = null;

            _DbContext.Transactions.Add(transaction);
            _DbContext.SaveChanges();
        }
        public void Update(Transaction transaction)
        {
            _DbContext.Transactions.Update(transaction);
            _DbContext.SaveChanges();
        }
        public void Delete(int Id)
        {
            var transaction = _DbContext.Transactions.FirstOrDefault(x=>x.TransactionId == Id);

            _DbContext.Transactions.Remove(transaction);
            _DbContext.SaveChanges();

        }
        
    }
}
