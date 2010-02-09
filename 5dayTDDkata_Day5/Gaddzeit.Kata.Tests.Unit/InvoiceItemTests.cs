using Gaddzeit.Kata.Domain;
using NUnit.Framework;

namespace Gaddzeit.Kata.Tests.Unit
{
    [TestFixture]
    public class InvoiceItemTests
    {
        [Test]
        public void InvoiceItemParametersMatchProperties()
        {
            const int quantity = 3;
            const decimal amount = 15.25M;
            var invoiceLineItem = new InvoiceItem(quantity, amount);

            Assert.AreEqual(quantity, invoiceLineItem.Quantity);
            Assert.AreEqual(amount, invoiceLineItem.Amount);
        }

        [Test]
        [ExpectedException(typeof(NullOrZeroConstructorParameterException))]
        public void InvoiceItemRequiresNonZeroQuantittOnCreation()
        {
            const int quantity = 0;
            const decimal amount = 15.25M;
            var invoiceLineItem = new InvoiceItem(quantity, amount);
        }

        [Test]
        [ExpectedException(typeof(NullOrZeroConstructorParameterException))]
        public void InvoiceItemRequiresNonZeroAmountOnCreation()
        {
            const int quantity = 4;
            const decimal amount = 0M;
            var invoiceLineItem = new InvoiceItem(quantity, amount);
        }

    }
}
