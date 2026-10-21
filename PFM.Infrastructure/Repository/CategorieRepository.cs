using PFM.Domain.Entites;
using PFM.Infrastructure.Context;
using PFM.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Infrastructure.Repository
{
    public class CategorieRepository: ICategorieRepository
    {
        private readonly AppDbContext _context;
        public CategorieRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<Categories> GetAll()
        {
            return _context.Categories.ToList();
        }
        public Categories GetById(int Id)
        {
            return _context.Categories.FirstOrDefault(x => x.CategoriesId == Id);
        }
        public void Create(Categories category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }
        public void Update(Categories category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }
        public void Delete(int Id)
        {
            var Categori = _context.Categories.FirstOrDefault(x=>x.CategoriesId == Id);
            _context.Categories.Remove(Categori);
            _context.SaveChanges();
        }
    }
}
