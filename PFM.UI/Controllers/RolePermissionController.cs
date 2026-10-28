using Microsoft.AspNetCore.Mvc;
using PFM.UI.Models;
using PFM.UI.Services;

namespace PFM.UI.Controllers
{
    public class RolePermissionController : Controller
    {
        private readonly RolePermissionServices _rolePermissionServices;
        public RolePermissionController(RolePermissionServices rolePermissionServices)
        {
            _rolePermissionServices = rolePermissionServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var rolePermissionModel = _rolePermissionServices.GetAll();

            return View(rolePermissionModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(RolePermissionModel rolePermissionModel)
        {
            if (!ModelState.IsValid)
            {
                return View(rolePermissionModel);
            }

            var result = _rolePermissionServices.Create(rolePermissionModel);


            if (!result)
            {

                return View(rolePermissionModel);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var rolePermissionModel = _rolePermissionServices.GetById(Id);

            return View(rolePermissionModel);
        }

        [HttpPost]
        public IActionResult Edit(RolePermissionModel rolePermissionModel)
        {

            if (!ModelState.IsValid)
            {
                return View(rolePermissionModel);
            }

            var result = _rolePermissionServices.Update(rolePermissionModel);

            if (!result)
            {

                return View(rolePermissionModel);
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var result = _rolePermissionServices.Delete(Id);

            return RedirectToAction("Index");
        }
    }
}
