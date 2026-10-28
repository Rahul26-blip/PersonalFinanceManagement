using PFM.Application.DTO;
using PFM.Application.IServices;
using PFM.Domain.Entites;
using PFM.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Application.Services
{
    public class RoleServices: IRoleServices
    {
        private readonly IRoleRepository _roleRepository;
        public RoleServices(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public void Create(RoleDTO roleDTO)
        {
            Role role = new Role();

            role.RoleName=roleDTO.RoleName;
            role.RoleCode=roleDTO.RoleCode;
            role.IsActive=roleDTO.IsActive;
            role.CreatedOn=DateTime.Now;
            role.UpdatedOn=null;

            _roleRepository.Create(role);
        }
        public void Update(RoleDTO roleDTO)
        {
            Role role = _roleRepository.GetById(roleDTO.RoleId);

            if (role == null)
            {
                throw new KeyNotFoundException("Role not found.");
            }

            role.RoleName = roleDTO.RoleName;
            role.RoleCode = roleDTO.RoleCode;
            role.IsActive = roleDTO.IsActive;
            role.UpdatedOn = DateTime.Now;

            _roleRepository.Update(role);
        }
        public RoleDTO GetById(int id)
        {
            // Find the Role in the database
            var role = _roleRepository.GetById(id);

            // Stop if the Role does not exist
            if (role == null)
            {
                return null;
            }

            // Return the Role data
            return new RoleDTO
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName,
                RoleCode = role.RoleCode,
                IsActive = role.IsActive,
                CreatedOn = role.CreatedOn,
                UpdatedOn = role.UpdatedOn
            };
        }
        public List<RoleDTO> GetAll()
        {
            List < Role > roles= _roleRepository.GetAll();

            return roles .Select(role=>new RoleDTO
            {
                RoleName = role.RoleName,
                RoleCode = role.RoleCode,
                RoleId = role.RoleId,
                CreatedOn = role.CreatedOn,
                UpdatedOn = role.UpdatedOn,
                IsActive = role.IsActive,

            }).ToList();
            
        }
        
        public void Delete(int Id)
        { 
            _roleRepository.Delete(Id);
        }
    }
}
