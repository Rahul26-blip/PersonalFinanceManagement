using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PFM.Application.DTO;
using PFM.Application.IServices;

namespace PFM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
            // Get UserId from the logged-in user's JWT token
            var userIdValue = User.FindFirst("UserId")?.Value;

            // Convert UserId from string to integer
            if (!int.TryParse(userIdValue, out int userId) || userId <= 0)
            {
                return Unauthorized("Valid UserId was not found in the token.");
            }

            // Get only this user's budgets
            var budget = _budgetServices.GetAll(userId);

            return Ok(budget);
        }

        [HttpGet("GetById/{Id}")]
        public IActionResult GetById(int Id)
        {
            var userIdValue = User.FindFirst("UserId")?.Value;

            if (!int.TryParse(userIdValue, out int userId) || userId <= 0)
            {
                return Unauthorized("Valid UserId was not found in the token.");
            }

            var budget = _budgetServices.GetById(Id, userId);

            if (budget == null)
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
        public IActionResult Update(int Id, BudgetDTO budgetDTO)
        {
            // Get the logged-in user's ID from the JWT token
            var userIdValue = User.FindFirst("UserId")?.Value;

            if (!int.TryParse(userIdValue, out int userId) || userId <= 0)
            {
                return Unauthorized("Valid UserId was not found in the token.");
            }

            // Check whether this budget belongs to the logged-in user
            var budget = _budgetServices.GetById(Id, userId);

            if (budget == null)
            {
                return NotFound("Budget not found.");
            }

            // Set the budget ID received from the URL
            budgetDTO.BudgetId = Id;

            // Prevent changing ownership through the request body
            budgetDTO.UserId = userId;

            // Update only after confirming ownership
            _budgetServices.Update(budgetDTO, userId);

            return Ok("Update successfully");
        }


        [HttpDelete("Delete/{Id}")]
        public IActionResult Delete(int Id)
        {
            // Get the logged-in user's ID from the JWT
            var userIdValue = User.FindFirst("UserId")?.Value;

            if (!int.TryParse(userIdValue, out int userId) || userId <= 0)
            {
                return Unauthorized("Valid UserId was not found in the token.");
            }

            // Check whether the budget belongs to this user
            var budget = _budgetServices.GetById(Id, userId);

            if (budget == null)
            {
                return NotFound("Budget not found.");
            }

            // Delete only after ownership is confirmed
            _budgetServices.Delete(Id);

            return Ok("Budget deleted successfully.");
        }

        //[HttpPut("Update")]
        //public IActionResult Update(int Id, BudgetDTO budgetDTO)
        //{
        //    budgetDTO.BudgetId = Id;

        //    var budget = _budgetServices.GetById(Id);

        //    _budgetServices.Update(budgetDTO);

        //    return Ok("Update sucessfully");
        //}

        //[HttpDelete("Delete/{Id}")]
        //public IActionResult Delete(int Id)
        //{

        //    _budgetServices.Delete(Id);

        //    return Ok("Delete Sucessfully");
        //}

    }
}
