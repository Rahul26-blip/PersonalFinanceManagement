using PFM.Application.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Application.IServices
{
    public interface IUserServices
    {
        List<UserDTO> GetAll();
        UserDTO GetById(int Id);
        void Update(UserDTO userDTO);
        void Delete(int Id);
        void Create(UserDTO userDTO);
        // thay login Dtos FOr register and ligin method
        
        void Register(RegisterUserDTO registerUserDTO);
        LoginResponseDTO Login(LoginDTO loginDTO);

    }
}
