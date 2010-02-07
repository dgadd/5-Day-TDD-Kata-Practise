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

                foreach(var item in _invoiceItems)
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

                foreach (var item in _invoiceItems)
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
                    foreach (var invoiceItem in _invoiceItems)
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

        public void AddLineItem(InvoiceItem invoiceItem)
        {
            _invoiceItems.Add(invoiceItem);
        }
    }
}