using Microsoft.AspNetCore.Mvc;
using PFM.UI.Models;
using PFM.UI.Services;

namespace PFM.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApiService _apiService;
        public LoginController(ApiService apiService)
        {
            _apiService = apiService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginModel);
            }

            try
            {

                var login = _apiService.Login(loginModel);

                if (login == null || string.IsNullOrWhiteSpace(login.Token))
                {
                    ViewBag.Error = "Login API returned no token.";
                    return View(loginModel);
                }

                HttpContext.Session.SetString("token", login.Token);
                HttpContext.Session.SetString("UserName", login.UserName);
                HttpContext.Session.SetString("UserCode", login.UserCode);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.GetBaseException().Message;
                return View(loginModel);
            }
        }


        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index","Login");
        }
        
    }
}
