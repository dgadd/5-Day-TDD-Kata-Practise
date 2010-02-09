using Gaddzeit.Kata.Domain;
namespace Gaddzeit.Kata.Repository
{
    public interface ITaxesRepository
    {
        ITaxesService GetTaxesService();
    }
}