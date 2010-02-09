using System;
using System.Collections.Generic;
using Gaddzeit.Kata.Domain;

namespace Gaddzeit.Kata.View
{
    public interface IInvoiceView
    {
        event EventHandler GetCustomer;
        event EventHandler AddInvoiceLine;
        event EventHandler CalculateTotals;
        event EventHandler SaveInvoice;
        string CustomerFirstName { set; }
        string CustomerLastName { set; }
        string CustomerCode { get; }
        int Quantity { get; }
        decimal Amount { get; }
        List<InvoiceItem> InvoiceLineItems { get; set; }
        decimal SubTotal { set; }
        List<TaxCalculation> TaxCalculations { get; set; }
        decimal Total { get; set; }
    }
}