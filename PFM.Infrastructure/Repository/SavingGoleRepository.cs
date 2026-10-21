using PFM.Domain.Entites;
using PFM.Infrastructure.Context;
using PFM.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PFM.Infrastructure.Repository
{
    public class SavingGoleRepository: ISavingGoleRepository
    {
        private readonly AppDbContext _Context;
        public SavingGoleRepository(AppDbContext context)
        {
            _Context = context;
        }
        public List<SavingGole> GetAll()
        {
            return _Context.SavingGoles.ToList();
        }
        public SavingGole GetById(int Id)
        {
            return _Context.SavingGoles.FirstOrDefault(x => x.SavingsGolesId == Id);
        }
        public void Create(SavingGole savingGole)
        {
            // Set the creation date
             savingGole.CreatedOn = DateTime.Now;

            // No update date when creating a new record
            savingGole.UpdatedOn = null;

            _Context.SavingGoles.Add(savingGole);
            _Context.SaveChanges();
        }
        public void Update(SavingGole savingGole)
        {
            _Context.SavingGoles.Update(savingGole);
            _Context.SaveChanges();
        }
        public void Delete(int Id)
        {
            var savingGole = _Context.SavingGoles.FirstOrDefault(x=>x.SavingsGolesId==Id);
            _Context.SavingGoles.Remove(savingGole);
            _Context.SaveChanges();
        }

    }
}
