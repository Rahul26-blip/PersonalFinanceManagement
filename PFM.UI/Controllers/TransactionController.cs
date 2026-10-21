using Microsoft.AspNetCore.Mvc;
using PFM.UI.Models;
using PFM.UI.Services;

namespace PFM.UI.Controllers
{
    public class TransactionController : Controller
    {
        private readonly TransactionServices _transactionServices;

        public TransactionController(TransactionServices transactionServices)
        {
                _transactionServices = transactionServices;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var transactionModel = _transactionServices.GetAll();
            return View(transactionModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(TransactionModel transactionModel)
        {
            if(!ModelState.IsValid)
            {
                return View(transactionModel);
            }
            var result = _transactionServices.Craete(transactionModel);

            if(!result)
            {
                return View(transactionModel);
            }
            return RedirectToAction("Index");

        }

        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var transactionModel  = _transactionServices.GetById(Id);
            return View(transactionModel);
        }

        [HttpPost]
        public IActionResult Edit(TransactionModel transactionModel)
        {
            if(!ModelState.IsValid)
            {
                return View(transactionModel);
            }

             var result = _transactionServices.Update(transactionModel);

            if(!result)
            {
                return View(transactionModel);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            var result = _transactionServices.Delete(Id);

            return RedirectToAction("Index");
        }

    }
}
