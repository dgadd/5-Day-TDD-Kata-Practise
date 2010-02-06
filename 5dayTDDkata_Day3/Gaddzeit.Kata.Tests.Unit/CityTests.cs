using System;
using Gaddzeit.Kata.Domain;
using NUnit.Framework;
using Rhino.Mocks;

namespace Gaddzeit.Kata.Tests.Unit
{
    [TestFixture]
    public class CityTests
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
        public void CityDelegatesAddedTaxesToInjectedTaxesService()
        {
            // expectations
            var pstTax = new Tax("PST", DateTime.Today, DateTime.Today.AddMonths(6), JurisdictionEnum.City, 5);
            _mockTaxesService.AddTax(pstTax);

            _mockRepository.ReplayAll();

            var city = new City("Winnipeg", _mockTaxesService);
            city.AddTax(pstTax);            
        }
    }
}
     