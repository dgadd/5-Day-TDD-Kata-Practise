using System;
using Gaddzeit.Kata.Domain;
using Gaddzeit.Kata.View;

namespace Gaddzeit.Kata.Presenter
{
    public class TaxesPresenter
    {
        private readonly ITaxesService _taxesService;
        private readonly ITaxesView _taxesView;

        public TaxesPresenter(ITaxesService taxesService, ITaxesView taxesView)
        {
            _taxesService = taxesService;
            _taxesView = taxesView;

            _taxesView.ShowTaxes += new System.EventHandler(TaxesViewShowTaxes);
            _taxesView.AddTax += new System.EventHandler(TaxesViewAddTax);
        }

        void TaxesViewAddTax(object sender, System.EventArgs e)
        {
            var tax = new Tax(_taxesView.TaxType, _taxesView.StartDate, _taxesView.EndDate, _taxesView.Jurisdiction,
                              _taxesView.Percent);
            _taxesService.AddTax(tax);
            DisplayAllTaxes();
        }

        void TaxesViewShowTaxes(object sender, System.EventArgs e)
        {
            DisplayAllTaxes();
        }

        private void DisplayAllTaxes()
        {
            _taxesView.TaxesDisplay = _taxesService.Taxes;
        }
    }
}