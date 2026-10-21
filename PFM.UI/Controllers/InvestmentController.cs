using Microsoft.AspNetCore.Mvc;
using PFM.UI.Models;
using PFM.UI.Services;

namespace PFM.UI.Controllers
{
    public class InvestmentController : Controller
    {
        private readonly InvestmentServices _investmentServices;
        public InvestmentController(InvestmentServices investmentServices)
        {
            _investmentServices = investmentServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var accountsModel = _investmentServices.GetAll();

            return View(accountsModel);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(InvestmentModel investmentModel)
        {
            if (!ModelState.IsValid)
            {
                return View(investmentModel);
            }

            var result = _investmentServices.Create(investmentModel);


            if (!result)
            {
                ViewBag.Error = "Account could not be created.";
                return View(investmentModel);
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var investmentModel = _investmentServices.GetById(Id);

            return View(investmentModel);
        }


        [HttpPost]
        public IActionResult Edit(InvestmentModel investmentModel)
        {

            if (!ModelState.IsValid)
            {
                return View(investmentModel);
            }

            var result = _investmentServices.Update(investmentModel);

            if (!result)
            {
                ViewBag.Error = "Account could not be created.";
                return View(investmentModel);
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var result = _investmentServices.Delete(Id);

            return RedirectToAction("Index");
        }
    }
}
