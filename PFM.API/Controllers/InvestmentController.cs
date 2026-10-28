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
    public class InvestmentController : ControllerBase
    {
        private readonly IInvestmentServices _services;
        public InvestmentController(IInvestmentServices services)
        {
            _services = services;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var investmentDTO = _services.GetAll();
            return Ok(investmentDTO);
        }
        [HttpGet("GetById/{Id}")]
        public IActionResult GetById(int Id)
        {
            var investmentDto = _services.GetById(Id);
            return Ok(investmentDto);
        }
        [HttpPost("Create")]
        public IActionResult Create(InvestmentDTO investmentDTO)
        {
            _services.Create(investmentDTO);
            return Ok("Create Record Sucessfullyt");
        }
        [HttpPut("Update/{Id}")]
        public IActionResult Update(int Id, InvestmentDTO investmentDTO)
        {
            var existingInvestment = _services.GetById(Id);

            investmentDTO.InvestmentId = Id;

            _services.Update(investmentDTO);

            return Ok("Update Record Sucessfully");
        }
        [HttpDelete("Delete/{Id}")]
        public IActionResult Delete(int Id)
        {
            _services.Delete(Id);
            return Ok("Delet Record Sucessfully");
        }
    }
}
