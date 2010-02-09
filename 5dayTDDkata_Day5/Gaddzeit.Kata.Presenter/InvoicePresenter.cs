using Gaddzeit.Kata.Domain;
using Gaddzeit.Kata.Repository;
using Gaddzeit.Kata.View;

namespace Gaddzeit.Kata.Presenter
{
    public class InvoicePresenter
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ITaxesRepository _taxesRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceView _invoiceView;
        private Invoice _invoice;

        public InvoicePresenter(ICustomerRepository customerRepository, ITaxesRepository taxesRepository, IInvoiceRepository invoiceRepository, IInvoiceView invoiceView)
        {
            _customerRepository = customerRepository;
            _taxesRepository = taxesRepository;
            _invoiceRepository = invoiceRepository;
            _invoiceView = invoiceView;

            _invoiceView.GetCustomer += new System.EventHandler(InvoiceViewGetCustomer);
            _invoiceView.AddInvoiceLine += new System.EventHandler(InvoiceViewAddInvoiceLine);
            _invoiceView.CalculateTotals += new System.EventHandler(InvoiceViewCalculateTotals);
            _invoiceView.SaveInvoice += new System.EventHandler(InvoiceViewSaveInvoice);
        }

        void InvoiceViewSaveInvoice(object sender, System.EventArgs e)
        {
            var invoice = GetInvoice();
            _invoiceRepository.SaveInvoice(invoice);
        }

        void InvoiceViewCalculateTotals(object sender, System.EventArgs e)
        {
            var invoice = GetInvoice();
            _invoiceView.SubTotal = invoice.SubTotal;
            _invoiceView.TaxCalculations = invoice.TaxCalculations;
            _invoiceView.Total = invoice.Total;
        }

        void InvoiceViewAddInvoiceLine(object sender, System.EventArgs e)
        {
            var invoice = GetInvoice();
            invoice.AddLineItem(new InvoiceItem(_invoiceView.Quantity, _invoiceView.Amount));
            _invoiceView.InvoiceLineItems = invoice.InvoiceItems;
        }

        private Invoice GetInvoice()
        {
            if (_invoice == null)
            {
                _invoice = new Invoice(_taxesRepository.GetTaxesService());
            }
            return _invoice;
        }

        void InvoiceViewGetCustomer(object sender, System.EventArgs e)
        {
            var customer = _customerRepository.FindCustomerByCode(_invoiceView.CustomerCode);
            _invoiceView.CustomerFirstName = customer.FirstName;
            _invoiceView.CustomerLastName = customer.LastName;
        }
    }
}