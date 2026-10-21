using PFM.Application.DTO;
using PFM.Application.IServices;
using PFM.Domain.Entites;
using PFM.Infrastructure.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PFM.Application.Services
{
    public class UserServices :IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordServices _passwordServices;
        private readonly IJWTServices _jWTServices;
        public UserServices(IUserRepository userRepository,IPasswordServices passwordServices,IJWTServices jWTServices)
        {
            _userRepository = userRepository;
            _passwordServices = passwordServices;
            _jWTServices = jWTServices;
        }
        public void Register(RegisterUserDTO registerUserDTO)
        {
            if(registerUserDTO.Password!= registerUserDTO.ConfirmPassword)
            {
                return;
            }

            User user = new User();

            user.UserName = registerUserDTO.UserName;
            user.UserCode = registerUserDTO.UserCode;
            user.PasswordHash = _passwordServices.HashPassword(registerUserDTO.Password);
            user.IsActive = true;
            user.CreatedOn = DateTime.Now;
            user.UpdatedOn = DateTime.Now;
            _userRepository.Create(user);
        }
        public LoginResponseDTO Login(LoginDTO loginDTO)
        {
            var user = _userRepository.GetByUsercode(loginDTO.UserCode);

            if (user == null)
            {
                return null;
            }
            if (user.IsActive == false)
            {
                return null;
            }

            bool Passwordvalid = _passwordServices.VerifyPassword(loginDTO.Password,user.PasswordHash);
            

            if (Passwordvalid == false)
            {
                return null;
            }
            //JWT
            string token = _jWTServices.GenerateToken(user.UserId,user.UserName,user.UserCode);

            return new LoginResponseDTO
            {

                UserId = user.UserId,
                UserName = user.UserName,
                UserCode = user.UserCode,
                Token =token
            };
            
            
        }
        public List<UserDTO> GetAll()
        {
            
            List<User> users = _userRepository.GetAll();

            return users.Select(user => new UserDTO
            {
                UserId = user.UserId,
                UserName = user.UserName,
                UserCode=user.UserCode,
                IsActive = user.IsActive,
                CreatedOn = user.CreatedOn,
                UpdatedOn = user.UpdatedOn,

            }).ToList();
            
        }

        public UserDTO GetById(int id)
        {
            var user =_userRepository.GetById(id);

            return new UserDTO
            {
                UserId = user.UserId,
                UserName = user.UserName,
                UserCode = user.UserCode,
                CreatedOn = user.CreatedOn,
                IsActive = user.IsActive,
                UpdatedOn = user.UpdatedOn,
                
            };
        }

        public void Create(UserDTO userDTO)
        {
            User user = new User();

            
            user.UserName = userDTO.UserName;
            user.UserCode = userDTO.UserCode;
            user.CreatedOn = userDTO.CreatedOn;
            user.UpdatedOn = null;
            user.IsActive = userDTO.IsActive;
            

            _userRepository.Create(user);
        }
        public void Update(UserDTO userDTO)
        {
            
            var user = _userRepository.GetById(userDTO.UserId);

            user.UserName = userDTO.UserName;
            user.UserCode=userDTO.UserCode;
            user.UpdatedOn=DateTime.Now;
            user.IsActive=userDTO.IsActive;
            

            _userRepository.Update(user);

        }
        public void Delete(int Id)
        {
            _userRepository.Delete(Id);
        }
    }
}
