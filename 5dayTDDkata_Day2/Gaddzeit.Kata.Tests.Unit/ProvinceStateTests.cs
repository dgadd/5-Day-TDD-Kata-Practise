using System;
using Gaddzeit.Kata.Domain;
using NUnit.Framework;
using Rhino.Mocks;

namespace Gaddzeit.Kata.Tests.Unit
{
    [TestFixture]
    public class ProvinceStateTests
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
        public void ProvinceStateDelegatesAddedTaxesToInjectedTaxesService()
        {
            // expectations
            var pstTax = new Tax("PST", DateTime.Today, DateTime.Today.AddMonths(6), JurisdictionEnum.ProvinceState);
            _mockTaxesService.AddTax(pstTax);

            _mockRepository.ReplayAll();

            var provinceState = new ProvinceState("MB", _mockTaxesService);
            provinceState.AddTax(pstTax);
        }
    }
}