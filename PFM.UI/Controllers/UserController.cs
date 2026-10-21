using Microsoft.AspNetCore.Mvc;
using PFM.UI.Models;
using PFM.UI.Services;

namespace PFM.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly UserServices _userServices;
        public UserController(UserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var accountsModel = _userServices.GetAll();

            return View(accountsModel);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userModel);
            }

            var result = _userServices.Create(userModel);


            if (!result)
            {
                
                return View(userModel);
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var investmentModel = _userServices.GetById(Id);

            return View(investmentModel);
        }


        [HttpPost]
        public IActionResult Edit(UserModel userModel)
        {

            if (!ModelState.IsValid)
            {
                return View(userModel);
            }

            var result = _userServices.Update(userModel);

            if (!result)
            {
               
                return View(userModel);
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var result = _userServices.Delete(Id);

            return RedirectToAction("Index");
        }
    }
}
