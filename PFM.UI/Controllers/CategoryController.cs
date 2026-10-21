using Microsoft.AspNetCore.Mvc;
using PFM.UI.Models;
using PFM.UI.Services;

namespace PFM.UI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryServices _categoryServices;
        public CategoryController (CategoryServices categoryServices)
        {
            _categoryServices = categoryServices;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var categoryModel = _categoryServices.GetAll();

            return View(categoryModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CategoryModel categoryModel)
        {
            if(!ModelState.IsValid)
            {
                return View(categoryModel);
            }
            var result = _categoryServices.Create(categoryModel);

            if(!result)
            {
                return View(categoryModel);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var categoryModel =  _categoryServices.GetById(Id);

            return View(categoryModel);
        }

        [HttpPost]
        public IActionResult Edit(CategoryModel categoryModel)
        {
            if(!ModelState.IsValid)
            {
                return View(categoryModel);
            }

            var result = _categoryServices.Update(categoryModel);

            return RedirectToAction("Index");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int Id)
        {
            var caregoryModel = _categoryServices.Delete(Id);

            return RedirectToAction("Index");
        }

    }
}
