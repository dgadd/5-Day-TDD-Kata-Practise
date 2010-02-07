using System;
using System.Collections.Generic;
using Gaddzeit.Kata.Domain;
using Gaddzeit.Kata.Presenter;
using Gaddzeit.Kata.View;
using NUnit.Framework;
using Rhino.Mocks;

namespace Gaddzeit.Kata.Tests.Unit
{
    [TestFixture]
    public class TaxesPresenterTests
    {
        private MockRepository _mockRepository;
        private ITaxesView _mockTaxesView;
        private ITaxesService _mockTaxesService;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new MockRepository();
            _mockTaxesService = _mockRepository.StrictMock<ITaxesService>();
            _mockTaxesView = _mockRepository.StrictMock<ITaxesView>();
        }

        [TearDown]
        public void TearDown()
        {
            _mockRepository.VerifyAll();
        }

        [Test]
        public void TaxesPresenterAttachesViewEventHandlersOnConstruction()
        {
            _mockTaxesView.ShowTaxes += null;
            LastCall.IgnoreArguments();
            _mockTaxesView.AddTax += null;
            LastCall.IgnoreArguments();

            _mockRepository.ReplayAll();

            var taxesPresenter = new TaxesPresenter(_mockTaxesService, _mockTaxesView);
        }

        [Test]
        public void ShowTaxesEventDisplayAllTaxes()
        {
            _mockTaxesView.ShowTaxes += null;
            var showTaxesEventRaiser = LastCall.IgnoreArguments().GetEventRaiser();
            _mockTaxesView.AddTax += null;
            LastCall.IgnoreArguments();
            var taxes = new List<Tax>();
            Expect.Call(_mockTaxesService.Taxes).Return(taxes);
            _mockTaxesView.TaxesDisplay = taxes;

            _mockRepository.ReplayAll();

            var taxesPresenter = new TaxesPresenter(_mockTaxesService, _mockTaxesView);
            showTaxesEventRaiser.Raise(_mockTaxesView, EventArgs.Empty);
        }

        [Test]
        public void AddTaxEventCallsITaxesServiceAddAndReassignsToGridWithExtraRow()
        {
            _mockTaxesView.ShowTaxes += null;
            LastCall.IgnoreArguments();
            _mockTaxesView.AddTax += null;
            var addTaxEventRaiser = LastCall.IgnoreArguments().GetEventRaiser();
            const string taxType = "pstTax";
            DateTime? startDate = DateTime.Today;
            DateTime? endDate = DateTime.Today.AddYears(1);
            const JurisdictionEnum jurisdiction = JurisdictionEnum.ProvinceState;
            const int percent = 5;
            Expect.Call(_mockTaxesView.TaxType).Return(taxType);
            Expect.Call(_mockTaxesView.StartDate).Return(startDate);
            Expect.Call(_mockTaxesView.EndDate).Return(endDate);
            Expect.Call(_mockTaxesView.Jurisdiction).Return(jurisdiction);
            Expect.Call(_mockTaxesView.Percent).Return(percent);
            var tax = new Tax(taxType, startDate, endDate, jurisdiction, percent);
            _mockTaxesService.AddTax(tax);
            var taxes = new List<Tax> { tax };
            Expect.Call(_mockTaxesService.Taxes).Return(taxes);
            _mockTaxesView.TaxesDisplay = taxes;

            _mockRepository.ReplayAll();

            var taxesPresenter = new TaxesPresenter(_mockTaxesService, _mockTaxesView);
            addTaxEventRaiser.Raise(_mockTaxesView, EventArgs.Empty);
        }
    }
}