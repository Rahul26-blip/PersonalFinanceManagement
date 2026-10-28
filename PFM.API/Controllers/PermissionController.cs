using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PFM.Application.DTO;
using PFM.Application.IServices;

namespace PFM.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionServices _permissionServices;
        public PermissionController(IPermissionServices permissionServices)
        {
            _permissionServices = permissionServices;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var permissionDto = _permissionServices.GetAll();
            return Ok(permissionDto);
        }

        [HttpGet("GetById/{Id}")]
        public IActionResult GetById(int Id)
        {
            var PermissionDto = _permissionServices.GetById(Id);
            return Ok(PermissionDto);
        }

        [HttpPost("Create")]
        public IActionResult Create(PermissionDTO permissionDto)
        {
            _permissionServices.Create(permissionDto);
            return Ok("Create sucessfully");
        }
        [HttpPut("Update/{Id}")]
        public IActionResult Update(PermissionDTO permissionDto, int Id)
        {
            var PermissionDto = _permissionServices.GetById(Id);

            permissionDto.PermissionId = Id;

            _permissionServices.Update(permissionDto);
            return Ok("Update Sucessfully");

        }

        [HttpDelete("Delete/{Id}")]
        public IActionResult Delete(int Id)
        {
            var existing = _permissionServices.GetById(Id);

            _permissionServices.Delete(Id);
            return Ok("Delete Sucessfully");
        }
    }
}
