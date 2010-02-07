using System;
using System.Collections.Generic;
using Gaddzeit.Kata.Domain;

namespace Gaddzeit.Kata.View
{
    public interface ITaxesView
    {
        event EventHandler ShowTaxes;
        event EventHandler AddTax;
        List<Tax> TaxesDisplay { get; set; }
        
        string TaxType { get; }
        DateTime? StartDate { get; }
        DateTime? EndDate { get; }
        JurisdictionEnum Jurisdiction { get; }
        int Percent { get; }
    }
}