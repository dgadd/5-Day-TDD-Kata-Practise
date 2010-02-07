using System;
using System.Collections.Generic;

namespace Gaddzeit.Kata.Domain
{
    public class City
    {
        private readonly string _name;
        private readonly ITaxesService _taxesService;

        public City(string name, ITaxesService taxesService)
        {
            _name = name;
            _taxesService = taxesService;
        }

        public List<Tax> Taxes
        {
            get
            {
                return _taxesService.TaxesByJurisdiction(JurisdictionEnum.City);
            }
        }

        public void AddTax(Tax tax)
        {
            _taxesService.AddTax(tax);
        }



    }
}