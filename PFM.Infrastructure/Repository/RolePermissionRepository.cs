using PFM.Domain.Entites;
using PFM.Infrastructure.Context;
using PFM.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace PFM.Infrastructure.Repository
{
    public class RolePermissionRepository: IRolePermissionRepository
    {
        private readonly AppDbContext _Context;
        public RolePermissionRepository(AppDbContext context)
        {
            _Context = context;
        }
        public List<RolePermission> GetAll()
        {
            return _Context.RolePermissions.ToList();
        }
        public RolePermission GetById(int Id)
        {
            return _Context.RolePermissions.FirstOrDefault(x => x.RolePermissionId == Id);
        }
        public void Create(RolePermission rolePermission)
        {
            _Context.RolePermissions.Add(rolePermission);
            _Context.SaveChanges();
        }
        public void Update(RolePermission rolePermission)
        {
            _Context.RolePermissions.Update(rolePermission);
            _Context.SaveChanges();
        }
        public void Delete(int Id)
        {
            var rolePermission = _Context.RolePermissions.FirstOrDefault(x=>x.RolePermissionId==Id);
            _Context.RolePermissions.Remove(rolePermission);
            _Context.SaveChanges();
        }
    }
}
