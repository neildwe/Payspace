using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Payspace.Assesment.Interface;
using Payspace.Assesment.Repository;
using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using Payspace.Assesment.ViewModel;

namespace Payspace.Assesment.Services
{
    public class Calculator : ICalculator
    {
        private IConfiguration configuration { get; set; }
        private CalculatorContext context { get; set; }

        public Calculator(string ConnectionString)
        {
            context = new CalculatorContext(ConnectionString);
            this.Init();
        }
        public Calculator(CalculatorContext Context)
        {
            this.context = Context;
            this.Init();
        }
        public Calculator(CalculatorContext Context, IConfiguration Configuration) : this(Context)
        {
            configuration = Configuration;
        }

        private void Init()
        {
            if (this.context.Database.EnsureCreated())
            {
                this.context.PostalCode.AddRange(initPostalCode);
                this.context.Calculations.AddRange(initCalculations);
                this.context.SaveChanges();
            }
        }

        private Payspace.Assesment.Entities.Calculations[] initCalculations
        {
            get {
                return new Payspace.Assesment.Entities.Calculations[]
                {
                     new Payspace.Assesment.Entities.Calculations()
                     {
                         Rate = 10,
                         From = 0,
                         To = 8350
                     },
                     new Payspace.Assesment.Entities.Calculations()
                     {
                         Rate = 15,
                         From = 8351,
                         To = 33950
                     },
                     new Payspace.Assesment.Entities.Calculations()
                     {
                         Rate = 25,
                         From = 33951,
                         To = 82250
                     },
                     new Payspace.Assesment.Entities.Calculations()
                     {
                         Rate = 28,
                         From = 82251,
                         To = 171550
                     },
                     new Payspace.Assesment.Entities.Calculations()
                     {
                         Rate = 33,
                         From = 171551,
                         To = 372950
                     },
                     new Payspace.Assesment.Entities.Calculations()
                     {
                         Rate = 35,
                         From = 372951,
                         To = double.MaxValue
                     },
                };
            }
        }
        private Payspace.Assesment.Entities.PostalCode[] initPostalCode
        {
            get
            {
                return new Payspace.Assesment.Entities.PostalCode[]
                {
                new Payspace.Assesment.Entities.PostalCode()
                {
                    CalcType = Enumerations.ECalculationType.Progressive,
                    Code = "7441"
                },
                new Payspace.Assesment.Entities.PostalCode()
                {
                    CalcType = Enumerations.ECalculationType.FlatValue,
                    Code = "A100"
                },
                new Payspace.Assesment.Entities.PostalCode()
                {
                    CalcType = Enumerations.ECalculationType.FlatRate,
                    Code = "7000"
                },
                new Payspace.Assesment.Entities.PostalCode()
                {
                    CalcType = Enumerations.ECalculationType.Progressive,
                    Code = "1000"
                },
                };
            }
        }

        public CalculationResult Calculation(string PostalCode, double AnnualIncome)
        {
            var results = new CalculationResult();
            try
            {
                double AnnualTaxIncome = 0;
                var PostalType = this.context.PostalCode.FirstOrDefault(c => c.Code == PostalCode).CalcType;
                switch (PostalType)
                {
                    case Enumerations.ECalculationType.FlatValue:
                        if (AnnualIncome >= 200000)
                        {
                            AnnualTaxIncome = 10000;
                        }
                        else if (AnnualIncome < 200000)
                        {
                            AnnualTaxIncome = (AnnualIncome * (5 / 100));
                        }
                        break;
                    case Enumerations.ECalculationType.FlatRate:
                        AnnualTaxIncome = AnnualIncome * Convert.ToDouble((17.5 / 100));
                        break;
                    case Enumerations.ECalculationType.Progressive:
                        this.context.Calculations.Where(c => (AnnualIncome >= c.From)).ToList().ForEach(c =>
                         {
                             if (AnnualIncome >= c.To)
                             {
                                 var tax = (c.To - c.From) * Convert.ToDouble((c.Rate / 100));
                                 AnnualTaxIncome += tax;
                                 results.Calculations.Add(new Calculations()
                                 {
                                     From = c.From,
                                     To = c.To,
                                     Rate = c.Rate,
                                     Tax = tax
                                 });
                             }
                             else if ((AnnualIncome >= c.From) && (AnnualIncome <= c.To))
                             {
                                 var tax = (AnnualIncome - c.From) * Convert.ToDouble((c.Rate / 100));
                                 AnnualTaxIncome += tax;
                                 results.Calculations.Add(new Calculations()
                                 {
                                     From = c.From,
                                     To = AnnualIncome,
                                     Rate = c.Rate,
                                     Tax = tax
                                 });
                             }
                         });
                        break;
                    default:
                        break;
                }

                results.AnnualIncome = AnnualIncome;
                results.AnnualTaxIncome = AnnualTaxIncome;

                return results;
            }
            catch(Exception e)
            {
                throw;
            }
            finally
            {
                this.context.AuditResults.Add(new Entities.AuditResults()
                {
                    AnnualIncome = results.AnnualIncome,
                    PostalCode = PostalCode,
                    RequestedDate = DateTime.Now,
                    Tax = results.AnnualTaxIncome
                });
                this.context.SaveChanges();
            }
        }

        public IEnumerable<Payspace.Assesment.ViewModel.PostalCode> PostalCodes()
        {
            return (from row in this.context.PostalCode.AsNoTracking()
                    select new ViewModel.PostalCode()
                    {
                        CalcType = row.CalcType.ToString(),
                        Code = row.Code
                    });
        }
    }
}
