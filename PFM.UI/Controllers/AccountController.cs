using Microsoft.AspNetCore.Mvc;
using PFM.UI.Models;
using PFM.UI.Services;

namespace PFM.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountServices _accountServices;
        public AccountController(AccountServices accountServices)
        {
            _accountServices = accountServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var accountsModel = _accountServices.GetAll();

            return View(accountsModel);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(AccountModel accountModel)
        {
            if(!ModelState.IsValid)
            {
                return View(accountModel);
            }

            var result = _accountServices.Create(accountModel);

            if (!result)
            {
                ViewBag.Error = "Account could not be created.";
                return View(accountModel);
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var accountModel = _accountServices.GetById(Id);

            return View(accountModel);
        }


        [HttpPost]
        public IActionResult Edit(AccountModel accountModel)
        {

            if(!ModelState.IsValid)
            {
                return View(accountModel);
            }

            var result = _accountServices.Update(accountModel);

            if (!result)
            {
                ViewBag.Error = "Account could not be created.";
                return View(accountModel);
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int Id)
        {
            var result = _accountServices.Delete(Id);

            TempData["Success"] = "Account deleted successfully.";


            return RedirectToAction("Index");
        }
    }
}
