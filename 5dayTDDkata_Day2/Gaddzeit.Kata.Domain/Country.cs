using System;
using System.Collections.Generic;
using Gaddzeit.Kata.Domain;

namespace Gaddzeit.Kata.Domain
{
    public class Country
    {
        private readonly ITaxesService _taxesService;

        public Country(string name, ITaxesService taxesService)
        {
            _taxesService = taxesService;
        }

        public List<Tax> Taxes
        {
            get
            {
                return _taxesService.TaxesByJurisdiction(JurisdictionEnum.Country);
            }
        }

        public void AddTax(Tax tax)
        {
            _taxesService.AddTax(tax);
        }
    }
}