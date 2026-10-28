
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using PFM.Application.IServices;

namespace PFM.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardServices _dashboardServices;

        public DashboardController(
            IDashboardServices dashboardServices)
        {
            _dashboardServices = dashboardServices;
        }

        [HttpGet("GetDashboard")]
        public IActionResult GetDashboard()
        {
            // Read the logged-in user's ID from the JWT claims
            string userIdValue =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? User.FindFirst("userId")?.Value ?? User.FindFirst("UserId")?.Value ?? User.FindFirst("sub")?.Value;

            // Stop if the token does not contain a usable user ID
            if (!int.TryParse(userIdValue, out int userId) || userId <= 0)
            {
                return Unauthorized("User ID was not found in the login token.");
            }

            // Return dashboard data for this user only
            var dashboard = _dashboardServices.GetDashboard(userId);

            return Ok(dashboard);
        }
    }
}
