using PFM.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Infrastructure.IRepository
{
    public interface ICategorieRepository
    {
        List<Categories> GetAll();
        Categories GetById(int id);
        void Create(Categories category);
        void Update(Categories category);
        void Delete(int Id);
    }
}
