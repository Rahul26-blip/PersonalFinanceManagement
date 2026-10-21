using Microsoft.AspNetCore.Mvc;
using PFM.UI.Models;
using PFM.UI.Services;

namespace PFM.UI.Controllers
{
    public class SavingGoleController : Controller
    {
        private readonly SavingGoleServices _savingGoleServices;
        public SavingGoleController(SavingGoleServices savingGoleServices)
        {
            _savingGoleServices = savingGoleServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var savingGoleModel = _savingGoleServices.GetAll();

            return View(savingGoleModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SavingGoleModel savingGoleModel)
        {
            if(!ModelState.IsValid)
            {
                return View(savingGoleModel);
            }

            var result = _savingGoleServices.Create(savingGoleModel);

            if(!result)
            {
                return View(savingGoleModel);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var savingGoleModel = _savingGoleServices.GetById(Id);
            return View(savingGoleModel);
        }

        [HttpPost]
        public IActionResult Edit(SavingGoleModel savingGoleModel)
        {
            if(!ModelState.IsValid)
            {
                return View(savingGoleModel);
            }

            var result = _savingGoleServices.Update(savingGoleModel);

            if(!result)
            {
                return View(savingGoleModel);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var result = _savingGoleServices.Delete(Id);

            return RedirectToAction("Index");
        }
    }
}
