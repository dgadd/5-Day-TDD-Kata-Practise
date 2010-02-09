using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gaddzeit.Kata.Domain;
using NUnit.Framework;

namespace Gaddzeit.Kata.Tests.Unit
{
    [TestFixture]
    public class CustomerTests
    {
        [Test]
        public void CustomerAddsAnInvoiceAndInvoiceIncrements()
        {
            var customer = new Customer();
            var invoice = new Invoice(new TaxesService());
            customer.AddInvoice(invoice);
       
            Assert.AreEqual(1, customer.Invoices.Count);
        }
    }
}
