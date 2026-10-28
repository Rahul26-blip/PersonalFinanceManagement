using Microsoft.AspNetCore.Mvc;
using PFM.UI.Models;
using PFM.UI.Services;

namespace PFM.UI.Controllers
{
    public class PermissionController : Controller
    {
        private readonly PermissionServices _permissionServices;
        public PermissionController(PermissionServices permissionServices)
        {
            _permissionServices = permissionServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var roleServices = _permissionServices.GetAll();

            return View(roleServices);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(PermissionModel permissionModel)
        {
            if (!ModelState.IsValid)
            {
                return View(permissionModel);
            }

            var result = _permissionServices.Create(permissionModel);


            if (!result)
            {

                return View(permissionModel);
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var permissionModel = _permissionServices.GetById(Id);

            return View(permissionModel);
        }


        [HttpPost]
        public IActionResult Edit(PermissionModel permissionModel)
        {

            if (!ModelState.IsValid)
            {
                return View(permissionModel);
            }

            var result = _permissionServices.Update(permissionModel);

            if (!result)
            {

                return View(permissionModel);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var result = _permissionServices.Delete(Id);

            return RedirectToAction("Index");
        }
    }
}
