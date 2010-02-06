using System;
using Gaddzeit.Kata.Domain;
using NUnit.Framework;

namespace Gaddzeit.Kata.Tests.Unit
{
    [TestFixture]
    public class CityTests
    {
        [Test]
        public void CityCanAccumulateTaxes()
        {
            var city = new City("Winnipeg");
            var pstTax = new Tax("PST", DateTime.Today, DateTime.Today.AddMonths(6));
            city.AddTax(pstTax);

            Assert.IsTrue(city.Taxes.Contains(pstTax));
        }

        [Test]
        [ExpectedException(typeof(DuplicateTaxesException))]
        public void CityRejectsDuplicateTaxes()
        {
            var city = new City("Winnipeg");
            var pstTax1 = new Tax("PST", DateTime.Today, DateTime.Today.AddMonths(6));
            var pstTax2 = new Tax("PST", DateTime.Today, DateTime.Today.AddMonths(6));
            city.AddTax(pstTax1);
            city.AddTax(pstTax2);
        }

        [Test]
        [ExpectedException(typeof(OverlappingTaxTypesException))]
        public void CityRejectsOverlappingTaxesPerTaxType()
        {
            var city = new City("Winnipeg");
            var pstTax1 = new Tax("PST", DateTime.Today, DateTime.Today.AddMonths(6));
            var pstTax2 = new Tax("PST", DateTime.Today.AddMonths(6), DateTime.Today.AddYears(1));
            city.AddTax(pstTax1);
            city.AddTax(pstTax2);
        }
    }
}
