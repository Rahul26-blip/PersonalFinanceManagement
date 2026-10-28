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
            // Step 1: Find the existing RolePermission record
            var item = _rolePermissionRepository.GetById(
                rolePermissionDTO.RolePermissionId);

            // Step 2: Check whether the record exists
            if (item == null)
            {
                throw new KeyNotFoundException("RolePermission not found.");
            }

            // Step 3: Update its properties
            item.RoleId = rolePermissionDTO.RoleId;
            item.PermissionId = rolePermissionDTO.PermissionId;
            item.IsActive = rolePermissionDTO.IsActive;
            item.UpdatedOn = DateTime.Now;

            // Step 4: Save through the repository
            _rolePermissionRepository.Update(item);
        }

        public void Delete(int Id)
        {
            _rolePermissionRepository.Delete(Id);
        }
    }
}
