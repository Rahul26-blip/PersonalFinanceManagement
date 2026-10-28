using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFM.Application.DTO;
using PFM.Application.IServices;

namespace PFM.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SavingGoleController : ControllerBase
    {
        private readonly ISavingGoleServices _savingGoleServices;
        public SavingGoleController(ISavingGoleServices savedSavingGoleServices)
        {
            _savingGoleServices = savedSavingGoleServices;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
          var SavingGole = _savingGoleServices.GetAll();
            return Ok(SavingGole);
        }
        [HttpGet("GetById/{Id}")]
        public IActionResult GetById(int Id)
        {
            var SavingGole = _savingGoleServices.GetById(Id);
            return Ok(SavingGole);
        }
        [HttpPost("Create")]
        public IActionResult Create(SavingGoleDTO savingGoleDTO)
        {
            _savingGoleServices.Create(savingGoleDTO);
            return Ok(savingGoleDTO);
        }
        [HttpPut("Update/{Id}")]
        public IActionResult Update(int Id,SavingGoleDTO savingGoleDTO)
        {
            savingGoleDTO.SavingsGolesId = Id;
            _savingGoleServices.Update(savingGoleDTO);
            return Ok(savingGoleDTO);
        }
        [HttpDelete("Delete/{Id}")]
        public IActionResult Delete(int Id)
        {
            var savingGole = _savingGoleServices.GetById(Id);

            _savingGoleServices.Delete(Id);
            return Ok(Id);
        }
    }
}
