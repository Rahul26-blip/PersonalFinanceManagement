using PFM.Domain.Entites;
using PFM.Infrastructure.Context;
using PFM.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace PFM.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }
        public User GetById(int Id)
        {
            return _context.Users.FirstOrDefault(x => x.UserId == Id);
        }
        public User GetByUsercode(string usercode)
        {
            return _context.Users.FirstOrDefault(x => x.UserCode == usercode);
        }
        public void Create(User user)
        {
            user.CreatedOn = DateTime.Now;
            user.UpdatedOn = null;
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
        public void Delete(int Id)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserId == Id);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            // Permanently delete the user.
            _context.Users.Remove(user);

            // SQL Server may reject this if related records exist.
            _context.SaveChanges();
        }
    }
    
}
