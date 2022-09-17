using LoanShark.Helpers;
using LoanShark.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LoanShark.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet]
        public IActionResult App()
        {
           Loan model = new Loan();
            
            model.Payment = 0;
            model.TotalInterest = 0;
            model.TotalCost = 0;
            model.Amount = 5000M;
            model.Rate = 1.5M;            
            model.Term = 10;
            

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult App(Loan loan)
        {
            LoanHelper loanHelper = new LoanHelper();
            Loan newLoan = loanHelper.GetPayments(loan);



            return View(newLoan);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}