using System;
using System.Collections.Generic;
using Gaddzeit.Kata.Domain;

namespace Gaddzeit.Kata.Domain
{
    public class TaxesService : ITaxesService
    {
        public TaxesService()
        {
            Taxes = new List<Tax>();
        }

        public List<Tax> Taxes { get; private set; }

        public void AddTax(Tax tax)
        {
            if (tax == null) throw new ArgumentNullException("tax");
            RejectDuplicateTaxes(tax);
            RejectOverlappingTaxes(tax);

            this.Taxes.Add(tax);
        }

        private void RejectOverlappingTaxes(Tax tax)
        {
            foreach (var currowTax in this.Taxes)
            {
                if (IsFutureTax(tax, currowTax)
                    && FutureTaxOverlapsEndDateOfCurrowTax(tax, currowTax))
                    throw new OverlappingTaxTypesException();

                if (IsEarlierTax(tax, currowTax)
                    && CurrowTaxOverlapsEndDateOfPreviousTax(tax, currowTax))
                    throw new OverlappingTaxTypesException();
            }
        }

        private static bool CurrowTaxOverlapsEndDateOfPreviousTax(Tax tax, Tax currowTax)
        {
            return currowTax.TaxType.Equals(tax.TaxType)
                   && currowTax.StartDate <= tax.EndDate;
        }

        private static bool IsEarlierTax(Tax tax, Tax currowTax)
        {
            return currowTax.TaxType.Equals(tax.TaxType)
                   && tax.StartDate.Value < currowTax.StartDate;
        }

        private static bool FutureTaxOverlapsEndDateOfCurrowTax(Tax tax, Tax currowTax)
        {
            return currowTax.TaxType.Equals(tax.TaxType)
                   && tax.StartDate <= currowTax.EndDate;
        }

        private static bool IsFutureTax(Tax tax, Tax currowTax)
        {
            return currowTax.TaxType.Equals(tax.TaxType)
                   && tax.StartDate.Value > currowTax.StartDate;
        }

        private void RejectDuplicateTaxes(Tax tax)
        {
            if (this.Taxes.Contains(tax))
                throw new DuplicateTaxesException();
        }
    }
}