using Microsoft.AspNetCore.Mvc;
using PFM.UI.Models;
using PFM.UI.Services;

namespace PFM.UI.Controllers
{
    public class BudgetController : Controller
    {
        private readonly BudgetServices _budgetServices;
        public BudgetController(BudgetServices budgetServices)
        {
            _budgetServices = budgetServices;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var budgetmodel = _budgetServices.GetAll();
            return View(budgetmodel);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(BudgetModel budgetModel)
        {
            if (!ModelState.IsValid)
            {
                return View(budgetModel);
            }
            var result = _budgetServices.Create(budgetModel);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var budgetMode = _budgetServices.GetById(Id);
            return View(budgetMode);
        }
        [HttpPost]
        public IActionResult Edit(BudgetModel budgetModel)
        {
            if(!ModelState.IsValid)
            {
                return View(budgetModel);
            }
            var result = _budgetServices.Update(budgetModel);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var result = _budgetServices.Delete(Id);

            return RedirectToAction("Index");
        }

    }
}
