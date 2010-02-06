using System;
using Gaddzeit.Kata.Domain;
using NUnit.Framework;
using Rhino.Mocks;

namespace Gaddzeit.Kata.Tests.Unit
{
    [TestFixture]
    public class CountryTests
    {
        private MockRepository _mockRepository;
        private ITaxesService _mockTaxesService;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new MockRepository();
            _mockTaxesService = _mockRepository.StrictMock<ITaxesService>();
        }

        [TearDown]
        public void TearDown()
        {
            _mockRepository.ReplayAll();
            _mockRepository.VerifyAll();
        }

        [Test]
        public void CountryDelegatesAddedTaxesToInjectedTaxesService()
        {
            // expectations
            var pstTax = new Tax("PST", DateTime.Today, DateTime.Today.AddMonths(6), JurisdictionEnum.Country, 5);
            _mockTaxesService.AddTax(pstTax);

            _mockRepository.ReplayAll();

            var country = new Country("MB", _mockTaxesService);
            country.AddTax(pstTax);
        }
    }
}