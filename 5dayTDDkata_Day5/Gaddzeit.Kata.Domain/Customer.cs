using System;
using System.Collections.Generic;

namespace Gaddzeit.Kata.Domain
{
    public class Customer
    {
        private List<Invoice> _invoices;

        public Customer()
        {
            _invoices = new List<Invoice>();
        }

        public List<Invoice> Invoices
        {
            get { return _invoices; }
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public void AddInvoice(Invoice invoice)
        {
            Invoices.Add(invoice);
        }

    }
}