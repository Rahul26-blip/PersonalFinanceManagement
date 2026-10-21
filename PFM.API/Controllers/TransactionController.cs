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
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionServices _transactionServices;
        public TransactionController(ITransactionServices transactionServices)
        {
            _transactionServices = transactionServices;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var transaction = _transactionServices.GetAll();
            return Ok(transaction);
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int Id)
        {
            var transaction = _transactionServices.GetById(Id);
            return Ok(transaction);
        }
        [HttpPost("Create")]
        public IActionResult Create(TransactionDTO transactionDTO)
        {
            var userIdValue = User.FindFirst("UserId")?.Value;

            if (!int.TryParse(userIdValue, out int userId) || userId <= 0)
            {
                return Unauthorized("User ID was not found in the login token.");
            }

            // Assign the authenticated user's ID.
            // Do not trust the UserId sent by the UI.
            transactionDTO.UserId = userId;

            _transactionServices.Create(transactionDTO);

            return Ok("Transaction created successfully.");
        }

        [HttpPut("Update")]
        public IActionResult Update(TransactionDTO transactionDTO)
        {
            var existingTransaction =_transactionServices.GetById( transactionDTO.TransactionId);

            _transactionServices.Update(transactionDTO);

            return Ok("Update Secussefully");
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int Id)
        {
            var transaction = _transactionServices.GetById(Id);
            _transactionServices.Delete(Id);
            return Ok("Delete Secussefully");
        }
    }

}
