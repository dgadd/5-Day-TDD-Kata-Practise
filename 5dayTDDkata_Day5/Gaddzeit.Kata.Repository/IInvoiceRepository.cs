using Gaddzeit.Kata.Domain;

namespace Gaddzeit.Kata.Repository
{
    public interface IInvoiceRepository
    {
        void SaveInvoice(Invoice invoice);
    }
}