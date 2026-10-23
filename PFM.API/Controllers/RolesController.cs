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
            var role = _roleServices.GetById(Id);

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
            var existingRole = _roleServices.GetById(Id);

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
    