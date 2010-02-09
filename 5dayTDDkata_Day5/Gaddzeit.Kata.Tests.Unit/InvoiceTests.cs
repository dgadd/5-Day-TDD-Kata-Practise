using System;
using Gaddzeit.Kata.Domain;
using NUnit.Framework;
using Rhino.Mocks;

namespace Gaddzeit.Kata.Tests.Unit
{
    [TestFixture]
    public class InvoiceTests
    {
        private MockRepository _mockRepository;
        private ITaxesService _mockTaxesService;
        private City _city;
        private ProvinceState _provinceState;
        private Country _country;

        [SetUp]
        public void Setup()
        {
            _mockRepository = new MockRepository();
            _mockTaxesService = _mockRepository.Stub<ITaxesService>();

            _city = new City("CityTax", _mockTaxesService);
            var cityTax = new Tax("CityTax", DateTime.Today, DateTime.Today.AddMonths(6), JurisdictionEnum.City, 2);
            _mockTaxesService.Taxes.Add(cityTax);

            _provinceState = new ProvinceState("ProvStateTax", _mockTaxesService);
            var provStateTax = new Tax("ProvStateTax", DateTime.Today, DateTime.Today.AddMonths(6), JurisdictionEnum.ProvinceState, 3);
            _mockTaxesService.Taxes.Add(provStateTax);

            _country = new Country("CountryTax", _mockTaxesService);
            var countryTax = new Tax("CountryTax", DateTime.Today, DateTime.Today.AddMonths(6), JurisdictionEnum.Country, 4);
            _mockTaxesService.Taxes.Add(countryTax);
        }

        [Test]
        public void InvoiceGetsTaxesFromITaxesService()
        {
            var invoice = new Invoice(_mockTaxesService);
            Assert.AreEqual(invoice.Taxes, _mockTaxesService.Taxes);
        }

        [Test]
        public void InvoiceSumOfInvoiceLineItemQtyMatchesTotalItemQuantity()
        {
            var invoice = GetInvoice();

            Assert.AreEqual(12, invoice.TotalItemQuantity);
        }

        [Test]
        public void InvoiceSubtotalEqualsSumOfItemQuantityTimesAmount()
        {
            var invoice = GetInvoice();

            Assert.AreEqual(95.00M, invoice.SubTotal);
        }

        [Test]
        public void InvoiceHasMultipleTaxCalcRowsWithTypeAndTaxAmount()
        {
            var invoice = GetInvoice();

            foreach (var taxCalculation in invoice.TaxCalculations)
            {
                var expectedAmount = invoice.SubTotal * taxCalculation.Tax.Percent * .01M;

                 switch (taxCalculation.Tax.Jurisdiction)
                {
                    case JurisdictionEnum.City:
                        Assert.AreEqual(expectedAmount, taxCalculation.Amount);
                        break;
                    case JurisdictionEnum.ProvinceState:
                        Assert.AreEqual(expectedAmount, taxCalculation.Amount);
                        break;
                    case JurisdictionEnum.Country:
                        Assert.AreEqual(expectedAmount, taxCalculation.Amount);
                        break;
                }
            }
        }

        [Test]
        public void InvoiceTotalIsSubTotalPlusSumOfTaxCalculations()
        {
            var invoice = GetInvoice();

            var expectedAmount = invoice.SubTotal;

            foreach (var taxCalculation in invoice.TaxCalculations)
                expectedAmount += taxCalculation.Amount;

            Assert.AreEqual(expectedAmount, invoice.Total);
        }

        private Invoice GetInvoice()
        {
            var invoice = new Invoice(_mockTaxesService);
            invoice.AddLineItem(new InvoiceItem(3, 15.00M));
            invoice.AddLineItem(new InvoiceItem(4, 10.00M));
            invoice.AddLineItem(new InvoiceItem(5, 2.00M));
            return invoice;
        }

    }
}
