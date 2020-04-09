using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payspace.Assesment.ViewModel
{
    public class CalculationResult
    {
        public CalculationResult()
        {
            Calculations = new List<Calculations>();
        }
        public double AnnualTaxIncome { get; internal set; }
        public double AnnualIncome { get; internal set; }

        public List<Calculations> Calculations { get; internal set; }
    }
}
