using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFM.Application.DTO;
using PFM.Application.IServices;

namespace PFM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleServices _roleServices;
        public RolesController(IRoleServices roleServices)
        {
            _roleServices = roleServices;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var role = _roleServices.GetAll();
            return Ok(role);
        }

        [HttpGet("GetById/{Id}")]
        public IActionResult GetById(int Id, RoleDTO roleDTO)
        {
            if (Id <= 0)
            {
                return BadRequest("Invalid Role Id.");
            }

            var role = _roleServices.GetById(Id);

            if (role == null)
            {
                return NotFound("Role not found.");
            }

            return Ok(role);


        }
        [HttpPost("Create")]
        public IActionResult Create(RoleDTO roleDTO)
        {
            _roleServices.Create(roleDTO);
            return Ok("Create Sucessfully");
        }

        [HttpPut("Update/{Id}")]
        public IActionResult Update(int Id,[FromBody]RoleDTO roleDTO)
        {
            if (roleDTO == null)
            {
                return BadRequest("Role data is required.");
            }

            var existingRole = _roleServices.GetById(Id);

            if (existingRole == null)
            {
                return NotFound("Role not found.");
            }

             roleDTO.RoleId = Id;
            _roleServices.Update(roleDTO);

            return Ok("Role updated successfully.");
        }

        [HttpDelete("Delete/{Id}")]
        public IActionResult Delete(int Id)
        {
            var existingRole = _roleServices.GetById(Id);

            _roleServices.Delete(Id);

            return Ok("Delete Sucessfully");
        }
    }
}
    