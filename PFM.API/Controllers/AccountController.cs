using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PFM.Application.DTO;
using PFM.Application.IServices;

namespace PFM.API.Controllers
{
    [Authorize]   
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServices _services;
        public AccountController(IAccountServices services)
        {
            _services = services;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var accountDTO = _services.GetAll();
            return Ok(accountDTO);
        }

        [HttpGet("GetById/{Id}")]
        public IActionResult GetById(int Id)
        {
            var accountDTO = _services.GetById(Id);

            if (accountDTO == null)
            {
                return null;
            }

            return Ok(accountDTO);


        }

        [HttpPost("Create")]
        public IActionResult Create(AccountDTO accountDTO)
        {
            
            _services.Create(accountDTO);

            return Ok("Create Sucessfully");
        }

        [HttpPut("Update")]
        public IActionResult Update(int Id, AccountDTO accountDTO)
        {
            accountDTO.AccountId = Id;

            var existingAccount = _services.GetById(Id);

            _services.Update(accountDTO);

            return Ok("Account updated successfully.");

        }
        [HttpDelete("Delete/{Id}")]
        public IActionResult Delete(int Id)
        {
            var account = _services.GetById(Id);

            _services.Delete(Id);
            return Ok("Account deleted successfully.");
        }

    }

}
