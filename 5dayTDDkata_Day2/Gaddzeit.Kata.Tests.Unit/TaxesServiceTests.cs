using System;
using System.Collections.Generic;
using Gaddzeit.Kata.Domain;
using NUnit.Framework;

namespace Gaddzeit.Kata.Tests.Unit
{
    [TestFixture]
    public class TaxesServiceTests
    {
        [Test]
        public void TaxesServiceCanAccumulateTaxes()
        {
            var taxesService = new TaxesService();
            var pstTax = new Tax("PST", DateTime.Today, DateTime.Today.AddMonths(6), JurisdictionEnum.City);
            taxesService.AddTax(pstTax);

            Assert.IsTrue(taxesService.Taxes.Contains(pstTax));
        }

        [Test]
        [ExpectedException(typeof(DuplicateTaxesException))]
        public void TaxesServiceRejectsDuplicateTaxes()
        {
            var taxesService = new TaxesService();
            var pstTax1 = new Tax("PST", DateTime.Today, DateTime.Today.AddMonths(6), JurisdictionEnum.City);
            var pstTax2 = new Tax("PST", DateTime.Today, DateTime.Today.AddMonths(6), JurisdictionEnum.City);
            taxesService.AddTax(pstTax1);
            taxesService.AddTax(pstTax2);
        }

        [Test]
        [ExpectedException(typeof(OverlappingTaxTypesException))]
        public void TaxesServiceRejectsOverlappingTaxesPerTaxType()
        {
            var taxesService = new TaxesService();
            var pstTax1 = new Tax("PST", DateTime.Today, DateTime.Today.AddMonths(6), JurisdictionEnum.City);
            var pstTax2 = new Tax("PST", DateTime.Today.AddMonths(6), DateTime.Today.AddYears(1), JurisdictionEnum.City);
            taxesService.AddTax(pstTax1);
            taxesService.AddTax(pstTax2);
        }

        [Test]
        public void TaxesServiceDistinguishesCorrectlyEachJurisdiction()
        {
            var taxesService = new TaxesService();

            var city = new City("CityTax", taxesService);
            var cityTax = new Tax("CityTax", DateTime.Today, DateTime.Today.AddMonths(6), JurisdictionEnum.City);
            city.AddTax(cityTax);

            var provinceState = new City("ProvStateTax", taxesService);
            var provStateTax = new Tax("ProvStateTax", DateTime.Today, DateTime.Today.AddMonths(6), JurisdictionEnum.ProvinceState);
            provinceState.AddTax(provStateTax);
            
            var country = new City("CountryTax", taxesService);
            var countryTax = new Tax("CountryTax", DateTime.Today, DateTime.Today.AddMonths(6), JurisdictionEnum.Country);
            country.AddTax(countryTax);

            Assert.AreEqual(1, city.Taxes.Count);
            Assert.AreEqual(1, provinceState.Taxes.Count);
            Assert.AreEqual(1, country.Taxes.Count);
        }

    }
}