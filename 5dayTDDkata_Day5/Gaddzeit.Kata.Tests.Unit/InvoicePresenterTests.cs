using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gaddzeit.Kata.Presenter;
using Gaddzeit.Kata.Repository;
using Gaddzeit.Kata.View;
using NUnit.Framework;
using Rhino.Mocks;
using Rhino.Mocks.Interfaces;
using Gaddzeit.Kata.Domain;

namespace Gaddzeit.Kata.Tests.Unit
{
    [TestFixture]
    public class InvoicePresenterTests
    {
        private MockRepository _mockRepository;
        private ICustomerRepository _mockCustomerRepository;
        private ITaxesRepository _mockTaxesRepository;
        private IInvoiceRepository _mockInvoiceRepository;
        private IInvoiceView _mockInvoiceView;

        [SetUp]
        public void SetUp()
        {
            _mockRepository = new MockRepository();
            _mockCustomerRepository = _mockRepository.StrictMock<ICustomerRepository>();
            _mockTaxesRepository = _mockRepository.StrictMock<ITaxesRepository>();
            _mockInvoiceRepository = _mockRepository.StrictMock<IInvoiceRepository>();
            _mockInvoiceView = _mockRepository.StrictMock<IInvoiceView>();
        }

        [TearDown]
        public void TearDown()
        {
            _mockRepository.VerifyAll();
        }

        [Test]
        public void InvoicePresenterAttachesAllViewEvents()
        {
            _mockInvoiceView.GetCustomer += null;
            LastCall.IgnoreArguments();
            _mockInvoiceView.AddInvoiceLine += null;
            LastCall.IgnoreArguments();
            _mockInvoiceView.CalculateTotals += null;
            LastCall.IgnoreArguments();
            _mockInvoiceView.SaveInvoice += null;
            LastCall.IgnoreArguments();

            _mockRepository.ReplayAll();

            var invoicePresenter = new InvoicePresenter(_mockCustomerRepository, _mockTaxesRepository, _mockInvoiceRepository, _mockInvoiceView);
        }

        [Test]
        public void InvoiceGetCustomerEventRetrievesCustomerInformation()
        {
            _mockInvoiceView.GetCustomer += null;
            var getCustomerEventRaiser = LastCall.IgnoreArguments().GetEventRaiser();
            _mockInvoiceView.AddInvoiceLine += null;
            LastCall.IgnoreArguments();
            _mockInvoiceView.CalculateTotals += null;
            LastCall.IgnoreArguments();
            _mockInvoiceView.SaveInvoice += null;
            LastCall.IgnoreArguments();

            const string customerCode = "JIMSMI";
            var customer = new Customer();
            Expect.Call(_mockInvoiceView.CustomerCode).Return(customerCode);
            Expect.Call(_mockCustomerRepository.FindCustomerByCode(customerCode)).Return(customer);
            _mockInvoiceView.CustomerFirstName = customer.FirstName;
            _mockInvoiceView.CustomerLastName = customer.LastName;

            _mockRepository.ReplayAll();

            var invoicePresenter = new InvoicePresenter(_mockCustomerRepository, _mockTaxesRepository, _mockInvoiceRepository, _mockInvoiceView);
            getCustomerEventRaiser.Raise(_mockInvoiceView, EventArgs.Empty);
        }

        [Test]
        public void InvoiceAddLineItemEventAddsLineItem()
        {
            _mockInvoiceView.GetCustomer += null;
            LastCall.IgnoreArguments();
            _mockInvoiceView.AddInvoiceLine += null;
            var addInvoiceLineEventRaiser = LastCall.IgnoreArguments().GetEventRaiser();
            _mockInvoiceView.CalculateTotals += null;
            LastCall.IgnoreArguments();
            _mockInvoiceView.SaveInvoice += null;
            LastCall.IgnoreArguments();

            const int quantity = 3;
            const decimal amount = 35.00M;
            ITaxesService taxesService = new TaxesService();
            Expect.Call(_mockInvoiceView.Quantity).Return(quantity);
            Expect.Call(_mockInvoiceView.Amount).Return(amount);
            Expect.Call(_mockTaxesRepository.GetTaxesService()).Return(taxesService);
            var invoiceItem = new InvoiceItem(quantity, amount);
            var invoice = new Invoice(taxesService);
            invoice.AddLineItem(invoiceItem);
            _mockInvoiceView.InvoiceLineItems = invoice.InvoiceItems;

            _mockRepository.ReplayAll();

            var invoicePresenter = new InvoicePresenter(_mockCustomerRepository, _mockTaxesRepository, _mockInvoiceRepository, _mockInvoiceView);
            addInvoiceLineEventRaiser.Raise(_mockInvoiceView, EventArgs.Empty);
        }

        [Test]
        public void InvoiceCalculateTotalsEventDisplaysSubTotalTaxesAndTotal()
        {
            _mockInvoiceView.GetCustomer += null;
            LastCall.IgnoreArguments();
            _mockInvoiceView.AddInvoiceLine += null;
            LastCall.IgnoreArguments();
            _mockInvoiceView.CalculateTotals += null;
            var calculateTotalsEventRaiser = LastCall.IgnoreArguments().GetEventRaiser();
            _mockInvoiceView.SaveInvoice += null;
            LastCall.IgnoreArguments();

            ITaxesService taxesService = new TaxesService();
            Expect.Call(_mockTaxesRepository.GetTaxesService()).Return(taxesService);
            var invoice = new Invoice(taxesService);
            _mockInvoiceView.SubTotal = invoice.SubTotal;
            _mockInvoiceView.TaxCalculations = invoice.TaxCalculations;
            _mockInvoiceView.Total = invoice.Total;

            _mockRepository.ReplayAll();

            var invoicePresenter = new InvoicePresenter(_mockCustomerRepository, _mockTaxesRepository, _mockInvoiceRepository, _mockInvoiceView);
            calculateTotalsEventRaiser.Raise(_mockInvoiceView, EventArgs.Empty);
        }

        [Test]
        public void InvoiceSaveInvoiceToRepository()
        {
            _mockInvoiceView.GetCustomer += null;
            LastCall.IgnoreArguments();
            _mockInvoiceView.AddInvoiceLine += null;
            LastCall.IgnoreArguments();
            _mockInvoiceView.CalculateTotals += null;
            LastCall.IgnoreArguments();
            _mockInvoiceView.SaveInvoice += null;
            var saveInvoiceEventRaiser = LastCall.IgnoreArguments().GetEventRaiser();

            ITaxesService taxesService = new TaxesService();
            Expect.Call(_mockTaxesRepository.GetTaxesService()).Return(taxesService);
            var invoice = new Invoice(taxesService);
            _mockInvoiceRepository.SaveInvoice(invoice);

            _mockRepository.ReplayAll();

            var invoicePresenter = new InvoicePresenter(_mockCustomerRepository, _mockTaxesRepository, _mockInvoiceRepository, _mockInvoiceView);
            saveInvoiceEventRaiser.Raise(_mockInvoiceView, EventArgs.Empty);
        }
    }
}
