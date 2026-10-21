using PFM.Application.DTO;
using PFM.Application.IServices;
using PFM.Domain.Entites;
using PFM.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Application.Services
{
    public class SavingGoleServices: ISavingGoleServices
    {
        private readonly ISavingGoleRepository _savingGoleRepository;
        public SavingGoleServices(ISavingGoleRepository savedGoleRepository)
        {
            _savingGoleRepository = savedGoleRepository;
        }
       public void Create(SavingGoleDTO savingGoleDTO)
        {
            SavingGole savingGole= new SavingGole();

            savingGole.UserId = savingGoleDTO.UserId;
            savingGole.GoleName = savingGoleDTO.GoleName;
            savingGole.TargetAmount = savingGoleDTO.TargetAmount;
            savingGole.CurrentAmount = savingGoleDTO.CurrentAmount;
            savingGole.TargetDate = savingGoleDTO.TargetDate;
            savingGole.IsCompleted = savingGoleDTO.IsCompleted;
            savingGole.IsActive = savingGoleDTO.IsActive;
            savingGole.CreatedOn = savingGoleDTO.CreatedOn;
            savingGole.UpdatedOn = savingGoleDTO.UpdatedOn;

            _savingGoleRepository.Create(savingGole);
        }
        public void Update(SavingGoleDTO savingGoleDTO)
        {
            var savingGole = _savingGoleRepository.GetById(savingGoleDTO.SavingsGolesId);

            savingGole.UserId = savingGoleDTO.UserId;
            savingGole.GoleName = savingGoleDTO.GoleName;
            savingGole.TargetAmount = savingGoleDTO.TargetAmount;
            savingGole.CurrentAmount = savingGoleDTO.CurrentAmount;
            savingGole.TargetDate = savingGoleDTO.TargetDate;
            savingGole.IsCompleted = savingGoleDTO.IsCompleted;
            savingGole.IsActive = savingGoleDTO.IsActive;
            savingGole.CreatedOn = savingGoleDTO.CreatedOn;
            savingGole.UpdatedOn = null ;

            _savingGoleRepository.Update(savingGole);


        }

        public List<SavingGoleDTO> GetAll()
        {
            List<SavingGole> savingGoles = new List<SavingGole>();

            var savingGole = _savingGoleRepository.GetAll();

            return savingGole.Select(savingGole => new SavingGoleDTO
            {

                SavingsGolesId = savingGole.SavingsGolesId,
                GoleName = savingGole.GoleName,
                UserId = savingGole.UserId,
                TargetAmount = savingGole.TargetAmount,
                CreatedOn = savingGole.CreatedOn,
                CurrentAmount = savingGole.CurrentAmount,
                TargetDate = savingGole.TargetDate,
                IsCompleted = savingGole.IsCompleted,
                IsActive = savingGole.IsActive,
                UpdatedOn = savingGole.UpdatedOn,
            }).ToList();
            
        }
        public SavingGoleDTO GetById(int Id)
        {

            var savingGole =_savingGoleRepository.GetById(Id);

            return new SavingGoleDTO
            {
                SavingsGolesId = savingGole.SavingsGolesId,
                GoleName = savingGole.GoleName,
                UserId = savingGole.UserId,
                TargetAmount = savingGole.TargetAmount,
                CreatedOn = savingGole.CreatedOn,
                CurrentAmount = savingGole.CurrentAmount,
                TargetDate = savingGole.TargetDate,
                IsCompleted = savingGole.IsCompleted,
                IsActive = savingGole.IsActive,
                UpdatedOn = savingGole.UpdatedOn,
            };
        }
        public void Delete(int Id)
        {
            _savingGoleRepository.Delete(Id);
        }

    }

}
