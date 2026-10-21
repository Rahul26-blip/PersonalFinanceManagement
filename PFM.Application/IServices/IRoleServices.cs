using PFM.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Application.IServices
{
    public interface IRoleServices
    {
        void Create(RoleDTO roleDTO);
        void Update(RoleDTO roleDTO);   
        void Delete(int  Id);
        List<RoleDTO> GetAll();
        RoleDTO GetById(int Id);
    }

}
