using PFM.Domain.Entites;
using PFM.Infrastructure.Context;
using PFM.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Infrastructure.Repository
{
    public class DebtRepository : IDebtRepository
    {
        private readonly AppDbContext _context;
        public DebtRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<Debts> GetAll()
        {
            return _context.Debts.ToList();
            
        }
        public Debts GetById(int Id)
        {
            return _context.Debts.FirstOrDefault(x => x.DebtsId == Id);
        }
        public void Create(Debts debts)
        {
            _context.Debts.Add(debts);
            _context.SaveChanges();
        }
        public void Update(Debts debts)
        {
            _context.Debts.Update(debts);
            _context.SaveChanges();

        }
        public void Delete(int Id)
        {
            var Debts = _context.Debts.FirstOrDefault(x => x.DebtsId == Id);
            _context.Debts.Remove(Debts);
            _context.SaveChanges();
        }

    }
}
