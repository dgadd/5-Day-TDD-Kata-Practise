using Gaddzeit.Kata.Domain;

namespace Gaddzeit.Kata.Repository
{
    public interface ICustomerRepository
    {
        Customer FindCustomerByCode(string customerCode);
    }
}