using PFM.Domain.Entites;
using PFM.Infrastructure.Context;
using PFM.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Infrastructure.Repository
{
    public class RoleRepository: IRoleRepository
    {
        private readonly AppDbContext _context;
        public RoleRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Create(Role role)
        {
            _context.Roles.Add(role);
            _context.SaveChanges();
        }
        public void Update(Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();

        }
        public List<Role> GetAll()
        {
            return _context.Roles.ToList();
        }
        public Role GetById(int id)
        {
            return _context.Roles.FirstOrDefault(x => x.RoleId == id);
        }
        public void Delete(int id)
        {
            var role = _context.Roles.FirstOrDefault(y => y.RoleId == id);

            if (role == null)
            {
                throw new KeyNotFoundException("Role not found.");
            }

            _context.Roles.Remove(role);
            _context.SaveChanges();
        }
    }
}
