using System;
using System.Collections;
using System.Collections.Generic;
using Gaddzeit.Kata.Domain;

namespace Gaddzeit.Kata.Domain
{
    public class Invoice
    {
        private readonly ITaxesService _taxesService;
        private readonly List<InvoiceItem> _invoiceItems;

        public Invoice(ITaxesService taxesService)
        {
            _taxesService = taxesService;
            _invoiceItems = new List<InvoiceItem>();
        }

        public List<Tax> Taxes
        {
            get { return _taxesService.Taxes; }
        }

        public int TotalItemQuantity
        {
            get
            {
                var totalItemQuantity = 0;

                foreach(var item in InvoiceItems)
                {
                    totalItemQuantity += item.Quantity;
                }
                return totalItemQuantity;
            }
        }

        public decimal SubTotal
        {
            get
            {
                var subTotal = 0M;

                foreach (var item in InvoiceItems)
                {
                    subTotal += (item.Quantity * item.Amount);
                }
                return subTotal;
            }
        }

        public List<TaxCalculation> TaxCalculations
        {
            get
            {
                var taxCalculations = new List<TaxCalculation>();
                foreach (var tax in _taxesService.Taxes)
                {
                    var taxCalculation = new TaxCalculation { Tax = tax };
                    foreach (var invoiceItem in InvoiceItems)
                    {
                        taxCalculation.Amount += invoiceItem.Amount * invoiceItem.Quantity * tax.Percent * .01M;
                    }
                    taxCalculations.Add(taxCalculation);
                }
                return taxCalculations;
            }
        }

        public decimal Total
        {
            get
            {
                var total = this.SubTotal;

                foreach (var taxCalculation in this.TaxCalculations)
                    total += taxCalculation.Amount;

                return total;
            }
        }

        public List<InvoiceItem> InvoiceItems
        {
            get { return _invoiceItems; }
        }


        public void AddLineItem(InvoiceItem invoiceItem)
        {
            InvoiceItems.Add(invoiceItem);
        }

        public override bool Equals(object obj)
        {
            var otherInvoice = (Invoice)obj;

            if(this.InvoiceItems.Count == 0
                && otherInvoice.InvoiceItems.Count == 0)
                return true;

            if (this.InvoiceItems.Count != otherInvoice.InvoiceItems.Count)
                return false;

            for(var i = 0; i < this.InvoiceItems.Count; i++)
            {
                if(!this.InvoiceItems[i].Equals(otherInvoice.InvoiceItems[i]))
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            if (this.InvoiceItems.Count == 0)
                return this.InvoiceItems.Count.GetHashCode();

            var totalHashCode = 0;
            foreach(var invoiceItem in this.InvoiceItems)
            {
                totalHashCode += invoiceItem.GetHashCode();
            }
            return totalHashCode;
        }
    }
}