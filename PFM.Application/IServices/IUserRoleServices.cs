using PFM.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Application.IServices
{
    public interface IUserRoleServices
    {
        List<UserRoleDTO> GetAll();
        UserRoleDTO GetById(int Id);
        void Update(UserRoleDTO userRoleDTO);
        void Delete(int Id);
        void Create(UserRoleDTO userRoleDTO);
    }
}
