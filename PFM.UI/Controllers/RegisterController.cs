using Microsoft.AspNetCore.Mvc;
using PFM.UI.Models;
using PFM.UI.Services;

namespace PFM.UI.Controllers
{
    public class RegisterController : Controller
    {

        private readonly ApiService _apiService;
        public RegisterController(ApiService apiService)
        {
            _apiService = apiService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
               return View(registerModel);
            }

            var register = _apiService.Register(registerModel);

            if(!register)
            {
                return View(registerModel);
            }

            return RedirectToAction("Index","Login");
        }

    }
}
