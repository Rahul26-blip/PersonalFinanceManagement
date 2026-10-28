    using PFM.Domain.Entites;
    using System;
    using System.Collections.Generic;
    using System.Text;

    namespace PFM.Infrastructure.IRepository
    {
        public interface IRolePermissionRepository
        {
            List<RolePermission> GetAll();
            RolePermission GetById(int Id);

            void Create(RolePermission rolePermission);
            void Update(RolePermission rolePermission);
            void Delete(int Id);
        }
    }
