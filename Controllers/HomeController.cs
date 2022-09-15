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
            model.Amount = 0;
            model.Term = 1;
            model.Rate = 0.5M;

            

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult App(Loan model)
        {      
            List<double> balanceArray = new List<double>();
            List<double> interestArray = new List<double>();
            List<double> principalpaymentArray = new List<double>();
            List<double> cumulativeArray = new List<double>();
            List<double> lItems = new List<double>();

           double A = Convert.ToDouble(model.Amount);
           double R = Convert.ToDouble(model.Rate);
           double T = Convert.ToDouble(model.Term);
           double balance = A;
            double cumulative = 0;
            double monthlyPayment = A * (R / 1200) / (1 - Math.Pow(1 + R / 1200, -T));

            for (int i = 0; i <= T; i++)
            {
                //math                
                double interest = balance * (R / 1200);
                double principalPayment = monthlyPayment - interest;                       
                double interestTotal = (balance + interest) - balance;
                balance = balance - principalPayment; 
                cumulative = cumulative + interest;                     

                //Pushing math to arrays.
                balanceArray.Add(balance);
                interestArray.Add(interestTotal);
                principalpaymentArray.Add(principalPayment);
                cumulativeArray.Add(cumulative);

                

            }
            double Cost = A + interestArray.Sum(x => Convert.ToDouble(x));
            model.TotalCost = Math.Round(Cost, 2); 
            model.TotalInterest = Math.Round(interestArray.Sum(x => Convert.ToDouble(x)),2); 
            model.TotalPrincipal = A;          
            model.Payment = Math.Round(monthlyPayment, 2);

            model.MonthlyPrincipal = principalpaymentArray;
            model.Balance = balanceArray;
            model.MonthlyInterest = interestArray;
            model.AccumulativeInterest = cumulativeArray;
            
            
            
            //model.Results = lItems;




            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}