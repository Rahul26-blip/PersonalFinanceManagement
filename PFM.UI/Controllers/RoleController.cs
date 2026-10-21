using Microsoft.AspNetCore.Mvc;
using PFM.UI.Models;
using PFM.UI.Services;

namespace PFM.UI.Controllers
{
    public class RoleController : Controller
    {

        private readonly RoleServices _roleServices;
        public RoleController(RoleServices roleServices)
        {
            _roleServices = roleServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var roleServices = _roleServices.GetAll();

            return View(roleServices);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(RoleModel roleModel)
        {
            if (!ModelState.IsValid)
            {
                return View(roleModel);
            }

            var result = _roleServices.Create(roleModel);

            if (!result)
            {

                return View(roleModel);
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int Id)
        {
            if (Id <= 0)
            {
                return BadRequest("Invalid Role ID: " + Id);
            }

            var roleModel = _roleServices.GetById(Id);

            if (roleModel == null)
            {
                return NotFound( $"Role ID {Id} was not returned by the API. Check GET /api/Roles/GetById/{Id}.");
            }

            return View(roleModel);
        }


        [HttpPost]
        public IActionResult Edit(RoleModel roleModel)
        {

            if (!ModelState.IsValid)
            {
                return View(roleModel);
            }

            var result = _roleServices.Update(roleModel);

            if (!result)
            {

                return View(roleModel);
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var result = _roleServices.Delete(Id);

            return RedirectToAction("Index");
        }
    }
}
