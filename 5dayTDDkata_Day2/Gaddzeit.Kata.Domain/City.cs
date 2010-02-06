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
            Taxes = new List<Tax>();
        }

        public List<Tax> Taxes { get; private set; }

        public void AddTax(Tax tax)
        {
            _taxesService.AddTax(tax);
        }



    }
}