using System.ComponentModel.DataAnnotations;

namespace LoanShark.Models
{
    public class Loan
    {
        public decimal Amount { get; set; }
        public int Term { get; set; }
        public decimal Rate { get; set; }
        [DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true) ]
        public List<double> Balance { get; set; } = new List<double>();
        public List<double> MonthlyPrincipal { get; set; } = new List<double>();
        public List<double> MonthlyInterest { get; set; } = new List<double>();
        public List<double> AccumulativeInterest { get; set; } = new List<double>();

        public double TotalPrincipal { get; set; }
        public double TotalInterest { get; set; }
        public double TotalCost { get; set; }
        public int Month { get; set; }
        public double Payment { get; set; }
        
        

    }
}
