using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PFM.Application.DTO;
using PFM.Application.IServices;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Pkcs;

namespace PFM.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserServices _userServices;
        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public IActionResult Register(RegisterUserDTO registerUserDTO)
        {
            _userServices.Register(registerUserDTO);
            return Ok(registerUserDTO);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(LoginDTO loginDTO)
        {
            var login = _userServices.Login(loginDTO);
            if(login == null)
            {
                return Unauthorized("Login failed. Check the User Code, password, and account status.");
            }
        
            return Ok(login);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var userDTO = _userServices.GetAll();
            return Ok(userDTO);
        }

        [HttpGet("GetById/{Id}")]
        public IActionResult GetById(int Id)
        {
            var userDTO = _userServices.GetById(Id);

            return Ok(userDTO);
        }

        [HttpPost("Create")]
        public IActionResult Create(UserDTO userDTO)
        {
            _userServices.Create(userDTO);

            return Ok("User Will Be Created");
        }

        [HttpPut("Update/{Id}")]
        public IActionResult Update(int Id, UserDTO userDTO)
        {
            var existingUser = _userServices.GetById(Id);

            userDTO.UserId = Id;

            _userServices.Update(userDTO);

            return Ok("Update User Sucessfully");

        }

        [HttpDelete("Delete/{Id}")]
        public IActionResult Delete(int Id)
        {
            var existingUser = _userServices.GetById(Id);

            _userServices.Delete(Id);

            return Ok("User Is Deleted");
        }

    }
}
