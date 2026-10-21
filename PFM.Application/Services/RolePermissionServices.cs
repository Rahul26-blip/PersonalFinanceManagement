using PFM.Application.DTO;
using PFM.Application.IServices;
using PFM.Domain.Entites;
using PFM.Infrastructure.IRepository;

namespace PFM.Application.Services
{
    public class RolePermissionServices : IRolePermissionServices
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;
        public RolePermissionServices(IRolePermissionRepository rolePermissionRepository)
        {
            _rolePermissionRepository = rolePermissionRepository;
        }
        public List<RolePermissionDTO> GetAll()
        {
            var rolePermission = _rolePermissionRepository.GetAll();
            return rolePermission.Select(rolePermissionDTO => new RolePermissionDTO
            {
                RolePermissionId = rolePermissionDTO.RolePermissionId,
                RoleId = rolePermissionDTO.RoleId,
                PermissionId = rolePermissionDTO.PermissionId,
                IsActive = rolePermissionDTO.IsActive,
                CreatedOn = rolePermissionDTO.CreatedOn,
                UpdatedOn = rolePermissionDTO.UpdatedOn,
            }).ToList();

        }
        public RolePermissionDTO GetById(int Id)
        {
            var item = _rolePermissionRepository.GetById(Id);

            if (item == null)
            {
                return null;
            }

            return new RolePermissionDTO
            {
                RolePermissionId = item.RolePermissionId,
                RoleId = item.RoleId,
                PermissionId = item.PermissionId,
                IsActive = item.IsActive,
                CreatedOn = item.CreatedOn,
                UpdatedOn = item.UpdatedOn
            };
        }
        public void Create(RolePermissionDTO rolePermissionDTO)
        {
            RolePermission rolePermission = new RolePermission();

            rolePermission.RoleId = rolePermissionDTO.RoleId;
            rolePermission.PermissionId = rolePermissionDTO.PermissionId;
            rolePermission.IsActive = rolePermissionDTO.IsActive;
            rolePermission.CreatedOn = DateTime.Now;
            rolePermission.UpdatedOn =null;

            _rolePermissionRepository.Create(rolePermission);
        }
        public void Update(RolePermissionDTO rolePermissionDTO)
        {
            var item = _rolePermissionRepository.GetById(rolePermissionDTO.RolePermissionId);

            item.RoleId = rolePermissionDTO.RoleId;
            item.PermissionId = rolePermissionDTO.PermissionId;
            item.IsActive = rolePermissionDTO.IsActive;
            item.UpdatedOn = DateTime.Now;

            _rolePermissionRepository.Update(item);
        }
        public void Delete(int Id)
        {
            _rolePermissionRepository.Delete(Id);
        }
    }
}
