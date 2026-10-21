using PFM.Application.DTO;

namespace PFM.Application.IServices
{
    public interface IRolePermissionServices
    {
        List<RolePermissionDTO> GetAll();
        RolePermissionDTO GetById(int Id);
        void Create(RolePermissionDTO rolePermissionDTO);
        void Update(RolePermissionDTO rolePermissionDTO);
        void Delete(int Id);
    }
}
