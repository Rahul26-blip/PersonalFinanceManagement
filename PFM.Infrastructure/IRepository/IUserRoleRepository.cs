using PFM.Domain.Entites;

namespace PFM.Infrastructure.IRepository
{
    public interface IUserRoleRepository
    {
        List<UserRole> GetAll();
        UserRole GetById(int Id);
        void Delete(int Id);
        void Create(UserRole transaction);
        void Update(UserRole transaction);
    }
}
