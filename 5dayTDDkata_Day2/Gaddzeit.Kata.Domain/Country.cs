using System;
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

        public void AddTax(Tax tax)
        {
            _taxesService.AddTax(tax);
        }
    }
}