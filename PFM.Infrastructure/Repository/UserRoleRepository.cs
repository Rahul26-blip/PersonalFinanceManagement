using PFM.Domain.Entites;
using PFM.Infrastructure.Context;
using PFM.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Infrastructure.Repository
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly AppDbContext _Context;
        public UserRoleRepository(AppDbContext context)
        {
            _Context = context;
        }
        public List<UserRole> GetAll()
        {
            return _Context.UserRoles.ToList();
        }
        public UserRole GetById(int Id)
        {
            return _Context.UserRoles.SingleOrDefault(x => x.UserRoleId == Id);
        }
        public void Create (UserRole userrole)
        {
            _Context.UserRoles.Add(userrole);
            _Context.SaveChanges();
        }
        public void Update (UserRole userrole)
        {
            _Context.UserRoles.Update(userrole);
            _Context.SaveChanges();
        }
        public void Delete(int Id)
        {
            var userrole = _Context.UserRoles.FirstOrDefault(x => x.UserRoleId == Id);
            _Context.UserRoles.Remove(userrole);
            _Context.SaveChanges();
        }
    }
}
