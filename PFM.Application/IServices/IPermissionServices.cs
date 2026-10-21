using PFM.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Application.IServices
{
    public interface IPermissionServices
    {
        List<PermissionDTO> GetAll();
        PermissionDTO GetById(int Id);
        
        void Create(PermissionDTO permissionDTO);
        void Update(PermissionDTO permissionDTO);
        void Delete(int Id);
    }
}
