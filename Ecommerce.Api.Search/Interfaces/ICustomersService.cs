using Ecommerce.Api.Search.Models;

namespace Ecommerce.Api.Search.Interfaces
{
    public interface ICustomersService
    {
        Task<(bool IsSuccess, IEnumerable<Customer> Customers, string ErrorMessage)> GetCustomersAsync();
    }
}
