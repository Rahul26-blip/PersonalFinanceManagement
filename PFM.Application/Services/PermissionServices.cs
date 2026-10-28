using PFM.Application.DTO;
using PFM.Application.IServices;
using PFM.Domain.Entites;
using PFM.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Application.Services
{
    public class PermissionServices: IPermissionServices
    {
        private readonly IPermissionRepository _permissionRepository;
        public PermissionServices(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }
        public List<PermissionDTO> GetAll()
        {
            List<Permission> permission = _permissionRepository.GetAll();

            return permission.Select(permissionDto => new PermissionDTO
            {
                PermissionId = permissionDto.PermissionId,
                PermissionName = permissionDto.PermissionName,
                PermissionCode = permissionDto.PermissionCode,
                IsActive = permissionDto.IsActive,
                CreatedOn = permissionDto.CreatedOn,
                UpdatedOn = permissionDto.UpdatedOn,
            }).ToList();
        }
        public PermissionDTO GetById(int Id)
        {
            var permission = _permissionRepository.GetById(Id);

            if (permission == null)
            {
                return null;
            }

            return new PermissionDTO
            {
                PermissionId = permission.PermissionId,
                PermissionName = permission.PermissionName,
                PermissionCode = permission.PermissionCode,
                IsActive = permission.IsActive,
                CreatedOn = permission.CreatedOn,
                UpdatedOn = permission.UpdatedOn,
            };
        }
        public void Create (PermissionDTO permissionDto)
        {
            Permission permission = new Permission();
            
            permission.PermissionName = permissionDto.PermissionName;
            permission.PermissionCode = permissionDto.PermissionCode;
            permission.IsActive = permissionDto.IsActive;
            permission.CreatedOn = DateTime.Now;
            permission.UpdatedOn =null;

            _permissionRepository.Create(permission);
        }

        public void Update(PermissionDTO permissionDto)
        {
            // Step 1: Get permission from the repository using its ID
            var permission = _permissionRepository.GetById(permissionDto.PermissionId);

            // Step 2: Check if permission exists
            if (permission == null)
            {
                throw new KeyNotFoundException("Permission not found.");
            }

            // Step 3: Update permission details
            permission.PermissionName = permissionDto.PermissionName;
            permission.PermissionCode = permissionDto.PermissionCode;
            permission.IsActive = permissionDto.IsActive;
            permission.UpdatedOn = DateTime.Now;

            // Step 4: Save changes through the repository
            _permissionRepository.Update(permission);
        }

        public void Delete(int Id)
        {
            _permissionRepository.Delete(Id);
        }
    }
}
