using PFM.Domain.Entites;
using PFM.Infrastructure.Context;
using PFM.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Infrastructure.Repository
{
    public class PermissionRepository :IPermissionRepository
    {
        private readonly AppDbContext _context;
        public PermissionRepository(AppDbContext context)
        {
            _context = context;
        }
        public List<Permission>GetAll()
        {
            return _context.Permissions.ToList();
        }
        public Permission GetById(int Id)
        {
            return _context.Permissions.FirstOrDefault(x => x.PermissionId == Id);
        }
        public void Create(Permission permission)
        {
            _context.Permissions.Add(permission);
            _context.SaveChanges();
        }
        public void Update(Permission permission)
        {
            _context.Permissions.Update(permission);
            _context.SaveChanges();
        }
        public void Delete(int Id)
        {
            var permission = _context.Permissions.FirstOrDefault(c=>c.PermissionId == Id);

            if (permission == null)
            {
                throw new KeyNotFoundException("Permission not found.");
            }

            _context.Permissions.Remove(permission);
            _context.SaveChanges();
        }
    }
}
