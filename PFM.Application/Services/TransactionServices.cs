using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using PFM.Application.DTO;
using PFM.Application.IServices;
using PFM.Domain.Entites;
using PFM.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Application.Services
{
    public class TransactionServices: ITransactionServices
    {
        private readonly ITransactionRepository _transactionRepository;
        public TransactionServices(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }
        public List<TransactionDTO> GetAll()
        {
            List<Transaction> transactions = _transactionRepository.GetAll();
            return transactions.Select(transaction => new TransactionDTO
            {
                TransactionId = transaction.TransactionId,
                UserId = transaction.UserId,
                AccountId=transaction.AccountId,
                CategoriesId = transaction.CategoriesId,
                TransactionType = transaction.TransactionType,
                Amount = transaction.Amount,
                TransactionDate = transaction.TransactionDate,
                Description = transaction.Description,
                IsActive = transaction.IsActive,
                CreatedOn = transaction.CreatedOn,
                UpdatedOn = transaction.UpdatedOn,
            }).ToList();
        }
        public TransactionDTO GetById(int Id)
        {
            var transaction = _transactionRepository.GetById(Id);

            return new TransactionDTO
            {
                TransactionId = transaction.TransactionId,
                UserId = transaction.UserId,
                AccountId = transaction.AccountId,
                CategoriesId = transaction.CategoriesId,
                TransactionType = transaction.TransactionType,
                Amount = transaction.Amount,
                TransactionDate = transaction.TransactionDate,
                Description = transaction.Description,
                IsActive = transaction.IsActive,
                CreatedOn = transaction.CreatedOn,
                UpdatedOn = transaction.UpdatedOn,

            };
        }

        public void Create(TransactionDTO transactionDTO)
        {
            // Validate the required transaction date.
            if (transactionDTO.TransactionDate == default(DateTime) ||
                transactionDTO.TransactionDate < new DateTime(1753, 1, 1))
            {
                throw new ArgumentException(
                    "Please select a valid Transaction Date.");
            }

            // Create a new entity.
            Transaction transaction = new Transaction();

            transaction.UserId = transactionDTO.UserId;
            transaction.AccountId = transactionDTO.AccountId;
            transaction.CategoriesId = transactionDTO.CategoriesId;
            transaction.TransactionType = transactionDTO.TransactionType;
            transaction.Amount = transactionDTO.Amount;

            // Save only the date; do not save the time.
            transaction.TransactionDate =
                transactionDTO.TransactionDate.Date;

            transaction.Description = transactionDTO.Description;
            transaction.IsActive = transactionDTO.IsActive;

            // Set audit dates.
            transaction.CreatedOn = DateTime.Now;
            transaction.UpdatedOn = null;

            _transactionRepository.Create(transaction);
        }

        public void Update(TransactionDTO transactionDTO)
        {

            var transaction = _transactionRepository.GetById(transactionDTO.TransactionId);

            transaction.TransactionId = transactionDTO.TransactionId;
            transaction.UserId = transactionDTO.UserId;
            transaction.CategoriesId = transactionDTO.CategoriesId;
            transaction.AccountId = transactionDTO.AccountId;
            transaction.TransactionType = transactionDTO.TransactionType;
            transaction.Amount = transactionDTO.Amount;
            transaction.TransactionDate = transactionDTO.TransactionDate;
            transaction.Description = transactionDTO.Description;
            transaction.IsActive = transactionDTO.IsActive;
            transaction.UpdatedOn = DateTime.Now;

            _transactionRepository.Update(transaction);
        }
        public void Delete(int Id)
        {
            _transactionRepository.Delete(Id);
        }


    }
}
