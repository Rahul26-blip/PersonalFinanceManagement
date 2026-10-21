using PFM.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Infrastructure.IRepository
{
    public  interface IPermissionRepository
    {
        List<Permission> GetAll();
        Permission GetById(int Id);

        void Create(Permission permission);
        void Update(Permission permission);
        void Delete(int Id);
    }
}
