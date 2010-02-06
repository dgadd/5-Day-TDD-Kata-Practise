using System;
using Gaddzeit.Kata.Domain;

namespace Gaddzeit.Kata.Domain
{
    public class ProvinceState
    {
        private readonly ITaxesService _taxesService;

        public ProvinceState(string name, ITaxesService taxesService)
        {
            _taxesService = taxesService;
        }

        public void AddTax(Tax tax)
        {
            _taxesService.AddTax(tax);
        }
    }
}