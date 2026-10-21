using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PFM.Application.DTO;
using PFM.Application.IServices;

namespace PFM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetServices _budgetServices;
        public BudgetController(IBudgetServices budgetServices)
        {
            _budgetServices = budgetServices;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var budget = _budgetServices.GetAll();
            return Ok(budget);
        }

        [HttpGet("GetById/{Id}")]
        public IActionResult GetById(int Id)
        {
            var budget = _budgetServices.GetById(Id);
            if(budget == null)
            {
                return BadRequest("Budget Not Found");
            }
            return Ok(budget);
        }

        [HttpPost("Create")]
        public IActionResult Create(BudgetDTO budgetDTO)
        {
            _budgetServices.Create(budgetDTO);
            return Ok("Create Sucessfully");
        }

        [HttpPut("Update")]
        public IActionResult Update(int Id,BudgetDTO budgetDTO)
        {
            budgetDTO.BudgetId = Id;

            var budget = _budgetServices.GetById(Id);

            _budgetServices.Update(budgetDTO);

            return Ok("Update sucessfully");
        }

        [HttpDelete("Delete/{Id}")]
        public IActionResult Delete(int Id)
        {
            
            _budgetServices.Delete(Id);

            return Ok("Delete Sucessfully");
        }

    }
}
