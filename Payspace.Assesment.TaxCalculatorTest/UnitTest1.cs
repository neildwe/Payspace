using NUnit.Framework;
using Payspace.Assesment.Interface;
using Payspace.Assesment.Services;
using Payspace.Assesment.ViewModel;
using System;

namespace Payspace.Assesment.TaxCalculatorTest
{
    public class Tests
    {

        private ICalculator Calculator { get; set; }
        private const string connectionString = "Server=localhost\\SQLEXPRESS;Database=Payspace;Trusted_Connection=True;";
        private double Income = 8350;

        [SetUp]
        public void Setup()
        {
            Calculator = new Calculator(connectionString);
        }

        [Timeout(1000)]
        [Test]
        public void TestProgressive()
        {
            var results = Calculator.Calculation("7441", Income);
            Assert.IsNotNull(results);
            validateProgressive(results, 835, 1);

            Income = Income * 2;
            results = Calculator.Calculation("7441", Income);
            Assert.IsNotNull(results);
            validateProgressive(results, 2087.35, 2);

            Income = Income * 2;
            results = Calculator.Calculation("7441", Income);
            Assert.IsNotNull(results);
            validateProgressive(results, 4592.35, 2);

            for(int i= 0; i <= 100; i++)
            {
                Income = Income * i;
                results = Calculator.Calculation("7441", Income);
                Assert.IsNotNull(results);
            }
        }

        private void validateProgressive(CalculationResult results, double annualTaxIncome, int CalculationsCount)
        {
            Assert.AreEqual(results.AnnualIncome, Income);
            Assert.AreEqual(Math.Round(results.AnnualTaxIncome, 2), Math.Round(annualTaxIncome, 2));
            Assert.AreEqual(results.Calculations.Count, CalculationsCount);
        }



        [Timeout(5000)]
        [Test]
        public void TestFlatValue()
        {
            var results = Calculator.Calculation("A100", Income);
            Assert.IsNotNull(results);
            Assert.AreEqual(results.AnnualIncome, Income);
            Assert.AreEqual(results.Calculations.Count, 0);
            Assert.AreEqual(results.AnnualTaxIncome, 0);
        }

        [Timeout(5000)]
        [Test]
        public void TestFlatRate()
        {
            var results = Calculator.Calculation("7000", Income);
            Assert.IsNotNull(results);
            Assert.AreEqual(results.AnnualIncome, Income);
            Assert.AreEqual(results.AnnualTaxIncome, 1461.25);
        }
    }
}