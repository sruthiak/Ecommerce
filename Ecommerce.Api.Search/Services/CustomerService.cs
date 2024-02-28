using Ecommerce.Api.Search.Interfaces;
using Ecommerce.Api.Search.Models;

namespace Ecommerce.Api.Search.Services
{
    public class CustomerService : ICustomersService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<CustomerService> logger;

        public CustomerService(IHttpClientFactory httpClientFactory,ILogger<CustomerService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }
        public Task<(bool IsSuccess, IEnumerable<Customer> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            { 

            }
            catch(Exception ex)
            {

            }
        }
    }
}
