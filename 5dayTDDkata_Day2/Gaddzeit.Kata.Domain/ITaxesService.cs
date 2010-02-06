using System.Collections.Generic;

namespace Gaddzeit.Kata.Domain
{
    public interface ITaxesService
    {
        void AddTax(Tax tax);
        List<Tax> Taxes { get; }
        List<Tax> TaxesByJurisdiction(JurisdictionEnum jurisdiction);
    }
}