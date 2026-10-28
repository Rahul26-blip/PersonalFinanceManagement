using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFM.Application.DTO;
using PFM.Application.IServices;
using PFM.Infrastructure.IRepository;

namespace PFM.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleServices _userRoleServices;
        public UserRoleController(IUserRoleServices userRoleServices)
        {
            _userRoleServices = userRoleServices;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var UserRoleDto = _userRoleServices.GetAll();
            return Ok(UserRoleDto);
        }

        [HttpGet("GetById/{Id}")]
        public IActionResult GetById(int Id)
        {
            var userRoleDto = _userRoleServices.GetById(Id);
            return Ok(userRoleDto);
        }

        [HttpPost("Create")]
        public IActionResult Create (UserRoleDTO userRoleDto)
        {
            _userRoleServices.Create(userRoleDto);
            return Ok("Create Record Sucessfully");
        }

        [HttpPut("Update/{Id}")]
        public IActionResult Update(int Id,UserRoleDTO userRoleDto)
        {
            userRoleDto.UserId = Id;
            _userRoleServices.Update(userRoleDto);
            return Ok("Update Recode Sucessfully");
        }

        [HttpDelete("Delete/{Id}")]
        public IActionResult Delete(int Id)
        {
            _userRoleServices.Delete(Id);
            return Ok("Delete Rocored Sucessfully");

        }

    }
}
