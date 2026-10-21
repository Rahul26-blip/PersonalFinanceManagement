using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFM.Application.DTO;
using PFM.Application.IServices;

namespace PFM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DebtsController : ControllerBase
    { 
        private readonly IDebtServices _services;
        public DebtsController(IDebtServices services)
        {
            _services = services;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var Debts = _services.GatAll();
            
            return Ok(Debts);
        }
        [HttpGet("GetById/{Id}")]
        public IActionResult GetById(int id)
        {
            var Debts = _services.GetById(id);

            return Ok(Debts);
        }
        [HttpPost("Create")]
        public IActionResult Create(DebtsDTO debtsDTO)
        {
             _services.Create(debtsDTO);
            return Ok("Craete Sucessfully");
        }
        [HttpPut("Update/{Id}")]
        public IActionResult Update(int Id,DebtsDTO debtsDTO)
        {
            var existingDebt = _services.GetById(Id);

            debtsDTO.DebtsId = Id;

            _services.Update(debtsDTO);
            return Ok("Update sesscefully");
        }
        [HttpDelete("Delete/{Id}")]
        public IActionResult Delete(int Id)
        {
            _services.Delete(Id);
            return Ok("Delete Sucessfully");
        }
    }

}
