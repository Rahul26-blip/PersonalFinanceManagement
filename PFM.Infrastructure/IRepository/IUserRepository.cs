using PFM.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Infrastructure.IRepository
{
    public interface IUserRepository
    {
        List<User> GetAll();
        User GetById(int Id);
        void Update(User user);
        void Delete(int Id);
        void Create(User user);

        User GetByUsercode(string usercode);
    }
}
