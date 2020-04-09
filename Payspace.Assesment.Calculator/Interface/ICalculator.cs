using Payspace.Assesment.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payspace.Assesment.Interface
{
    public interface ICalculator
    {
        CalculationResult Calculation(string PostalCode, double AnnualIncome);
        IEnumerable<Payspace.Assesment.ViewModel.PostalCode> PostalCodes();
    }
}
