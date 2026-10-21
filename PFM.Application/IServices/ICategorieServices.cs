using PFM.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Application.IServices
{
    public interface ICategorieServices
    {
        void Create(CategorieDTO categorieDTO);
        void Update(CategorieDTO categorieDTO);
        void Delete(int Id);
        List<CategorieDTO> GetAll();
        CategorieDTO GetById(int Id);
    }
}
