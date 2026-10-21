using Microsoft.AspNetCore.Mvc;
using PFM.UI.Models;
using PFM.UI.Services;

namespace PFM.UI.Controllers
{
    public class UserRoleController : Controller
    {
        private readonly UserRoleServices _userRoleServices;
        public UserRoleController(UserRoleServices userRoleServices)
        {
            _userRoleServices = userRoleServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var roleServices = _userRoleServices.GetAll();

            return View(roleServices);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(UserRoleModel userRoleModel)
        {
            if (!ModelState.IsValid)
            {
                return View(userRoleModel);
            }

            var result = _userRoleServices.Create(userRoleModel);


            if (!result)
            {

                return View(userRoleModel);
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var userRoleModel = _userRoleServices.GetById(Id);

            return View(userRoleModel);
        }


        [HttpPost]
        public IActionResult Edit(UserRoleModel userRoleModel)
        {

            if (!ModelState.IsValid)
            {
                return View(userRoleModel);
            }

            var result = _userRoleServices.Update(userRoleModel);

            if (!result)
            {

                return View(userRoleModel);
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var result = _userRoleServices.Delete(Id);

            return RedirectToAction("Index");
        }
    }
}
