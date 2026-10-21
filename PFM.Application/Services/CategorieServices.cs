using PFM.Application.DTO;
using PFM.Application.IServices;
using PFM.Domain.Entites;
using PFM.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Application.Services
{
    public class CategorieServices: ICategorieServices
    {
        private readonly ICategorieRepository _categorieRepository;
        public CategorieServices(ICategorieRepository categorieRepository)
        {
            _categorieRepository = categorieRepository;
        }

        public List<CategorieDTO> GetAll()
        {
            List<Categories> categories = _categorieRepository.GetAll();
            return categories.Select(categories => new CategorieDTO
            {
                CategoriesId = categories.CategoriesId,
                UserId = categories.UserId,
                CategoriesName = categories.CategoriesName,
                CategoriesCode = categories.CategoriesCode,
                IsActive = categories.IsActive,
                CreatedOn = categories.CreatedOn,
                UpdatedOn = categories.UpdatedOn,

            }).ToList();
        }
        public CategorieDTO GetById(int Id)
        {
            var categories = _categorieRepository.GetById(Id);

            if (categories == null)
            {
                return null;
            }

            return new CategorieDTO
            {
                CategoriesId = categories.CategoriesId,
                UserId = categories.UserId,
                CategoriesName = categories.CategoriesName,
                CategoriesCode = categories.CategoriesCode,
                IsActive = categories.IsActive,
                CreatedOn = categories.CreatedOn,
                UpdatedOn = categories.UpdatedOn,

            };
        }
        public void Create(CategorieDTO categorieDTO)
        {
            Categories categories = new Categories();
            categories.UserId = categorieDTO.UserId;
            categories.CategoriesName = categorieDTO.CategoriesName;
            categories.CategoriesCode = categorieDTO.CategoriesCode;
            categories.IsActive = categorieDTO.IsActive;
            categories.CreatedOn = DateTime.Now;
            categories.UpdatedOn = null;
            _categorieRepository.Create(categories);
        }
        public void Update(CategorieDTO categorieDTO)
        {
            var categories = _categorieRepository.GetById(categorieDTO.CategoriesId);

            categories.UserId = categorieDTO.UserId;
            categories.CategoriesName = categorieDTO.CategoriesName;
            categories.CategoriesCode = categorieDTO.CategoriesCode;
            categories.IsActive = categorieDTO.IsActive;
            categories.UpdatedOn = DateTime.Now;

            _categorieRepository.Update(categories);

        }
        public void Delete(int Id)
        {
            _categorieRepository.Delete(Id);
        }
    }
}
