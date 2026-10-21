using PFM.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Infrastructure.IRepository
{
    public interface IRoleRepository
    {
        List<Role> GetAll();
        Role GetById(int id);
        void Delete(int id);
        void Update(Role role);
        void Create(Role role);

    }
}
