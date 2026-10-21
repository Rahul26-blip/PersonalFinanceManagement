using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFM.Application.DTO;
using PFM.Application.IServices;

namespace PFM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePermissionController : ControllerBase
    {
        private readonly IRolePermissionServices _rolePermissionServices;
        public RolePermissionController(IRolePermissionServices rolePermissionServices)
        {
            _rolePermissionServices = rolePermissionServices;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var rolepermissionDto = _rolePermissionServices.GetAll();
            return Ok(rolepermissionDto);
        }
        [HttpGet("GetById/{Id}")]
        public IActionResult GetById(int Id)
        {

            var rolePermissionDTO = _rolePermissionServices.GetById(Id);

            return Ok(rolePermissionDTO);
        }

        [HttpPost("Create")]
        public IActionResult Create(RolePermissionDTO rolePermissionDTO)
        {
            _rolePermissionServices.Create(rolePermissionDTO);
            return Ok("CReate Sucessfully");
        }

        [HttpPut("Update/{Id}")]
        public IActionResult Update(int Id,[FromBody]RolePermissionDTO rolePermissionDTO)
        {
            var rolepermissionDto = _rolePermissionServices.GetById(Id);

            rolePermissionDTO.RolePermissionId = Id;
            _rolePermissionServices.Update(rolePermissionDTO);
            return Ok("Update Scucssfully");

        }

        [HttpDelete("Delete/{Id}")]
        public IActionResult Delete(int Id)
        {
            var existing = _rolePermissionServices.GetById(Id);

            _rolePermissionServices.Delete(Id);
            return Ok("Delete Sucessfully");
        }
    }
}
