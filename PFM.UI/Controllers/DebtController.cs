using Microsoft.AspNetCore.Mvc;
using PFM.UI.Models;
using PFM.UI.Services;

namespace PFM.UI.Controllers
{
    public class DebtController : Controller
    {
        private readonly DebtServices _debtservices;
        public DebtController(DebtServices debtservices)
        {
            _debtservices = debtservices;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var debtModel = _debtservices.GetAll();
            return View(debtModel);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DebtModel debtModel)
        {
            if(!ModelState.IsValid)
            {
                return View(debtModel);
            }

            var result = _debtservices.Create(debtModel);

            if(!result)
            {
                return View(debtModel);
            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var debtModel = _debtservices.GetById(Id);

            return View(debtModel);
        }
        [HttpPost]
        public IActionResult Edit(DebtModel debtModel)
        {
            if(!ModelState.IsValid)
            {
                return View(debtModel);
            }

            //var result = _debtservices.Update(debtModel);
            var result = _debtservices.Update(debtModel);

            if (!result)
            {
                return View(debtModel);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var result = _debtservices.Delete(Id);

            return RedirectToAction("Index");
        }
    }
}
