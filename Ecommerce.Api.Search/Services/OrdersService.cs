using Ecommerce.Api.Search.Interfaces;
using Ecommerce.Api.Search.Models;
using System.Text.Json;

namespace Ecommerce.Api.Search.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<OrdersService> logger;

        public OrdersService(IHttpClientFactory httpClientFactory,ILogger<OrdersService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }

        

        public async Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                var client = httpClientFactory.CreateClient("OrderServices");
                var response=await client.GetAsync($"/api/Orders/{customerId}");
                if(response.IsSuccessStatusCode)
                {
                    var content=await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
                    var result=JsonSerializer.Deserialize<IEnumerable<Order>>(content, options);
                    return (true, result, string.Empty);
                }
                return (false, Enumerable.Empty<Models.Order>(), response.ReasonPhrase);
            }
            catch(Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, Enumerable.Empty<Models.Order>(), ex.Message);
            }
        }
    }
}
