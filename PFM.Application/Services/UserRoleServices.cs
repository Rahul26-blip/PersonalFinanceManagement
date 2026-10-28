using PFM.Application.DTO;
using PFM.Application.IServices;
using PFM.Domain.Entites;
using PFM.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Application.Services
{
    public class UserRoleServices : IUserRoleServices 
    {
        private readonly IUserRoleRepository _repository;
        public UserRoleServices(IUserRoleRepository repository)
        {
            _repository = repository;
        }
        public List<UserRoleDTO> GetAll()
        {
            List<UserRole> userRoles = _repository.GetAll();
            return userRoles.Select(userRole => new UserRoleDTO
            {
                UserRoleId = userRole.UserRoleId,
                UserId = userRole.UserId,
                RoleId = userRole.RoleId,
                IsActive = userRole.IsActive,
                CreatedOn = userRole.CreatedOn,
                UpdatedOn = userRole.UpdatedOn,
            }).ToList();

        }
        public UserRoleDTO GetById(int id)
        {
            var userrole = _repository.GetById(id);

            if (userrole == null)
            {
                return null;
            }

            return new UserRoleDTO
            {
                UserRoleId = userrole.UserRoleId,
                UserId = userrole.UserId,
                RoleId = userrole.RoleId,
                IsActive = userrole.IsActive,
                CreatedOn = userrole.CreatedOn,
                UpdatedOn = userrole.UpdatedOn,
            };
        }
        public void Create(UserRoleDTO userRoleDTO)
        {
            UserRole userRole = new UserRole();

            userRole.UserId = userRoleDTO.UserId;
            userRole.RoleId = userRoleDTO.RoleId;
            userRole.IsActive = userRoleDTO.IsActive;
            userRole.CreatedOn = userRoleDTO.CreatedOn;
            userRole.UpdatedOn = userRoleDTO.UpdatedOn;

            _repository.Create(userRole);
        }
        public void Update(UserRoleDTO userRoleDTO)
        {
            // Get the existing record from the database.
            var userRole = _repository.GetById(userRoleDTO.UserRoleId);

            // Check whether the record exists.
            if (userRole == null)
            {
                throw new KeyNotFoundException("UserRole not found.");
            }

            // Update the existing object.
            userRole.UserId = userRoleDTO.UserId;
            userRole.RoleId = userRoleDTO.RoleId;
            userRole.IsActive = userRoleDTO.IsActive;

            // Set the current update date and time.
            userRole.UpdatedOn = DateTime.Now;

            // Save the updated object.
            _repository.Update(userRole);
        }
        public void Delete(int Id)
        {
            _repository.Delete(Id);
        }
    }
}
