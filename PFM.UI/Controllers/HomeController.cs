
using Microsoft.AspNetCore.Mvc;
using PFM.UI.Models;
using PFM.UI.Services;
using System.Diagnostics;

namespace PFM.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly DashboardServices _dashboardServices;

        public HomeController(DashboardServices dashboardServices)
        {
            _dashboardServices = dashboardServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                // Get dashboard data from the API
                DashboardModel dashboard =_dashboardServices.GetDashboard();

                // Send data to the Dashboard view
                return View(dashboard);
            }
            catch (Exception ex)
            {
                // Display the actual error while debugging
                ViewBag.DashboardError = ex.GetBaseException().Message;

                // Keep the page available if the API fails
                return View(new DashboardModel());
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0,Location = ResponseCacheLocation.None,NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
