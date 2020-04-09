using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Payspace.Assesment.Interface;
using Payspace.Assesment.Repository;
using Payspace.Assesment.Services;
using Payspace.Assesment.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payspace.Assesment.Controller
{

    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private ICalculator calculator { get; set; }
        public CalculatorController(CalculatorContext Context, IConfiguration Configuration)
        {
            calculator = new Calculator(Context, Configuration);
        }

        

        [HttpGet("")]
        [Route("Calculation")]
        public CalculationResult Calculation(string PostalCode, double AnnualIncome)
        {
            return calculator.Calculation(PostalCode, AnnualIncome);
        }

        [HttpGet("")]
        [Route("PostalCodes")]
        public IEnumerable<Payspace.Assesment.ViewModel.PostalCode> PostalCodes()
        {
            return calculator.PostalCodes();
        }
    }
}
