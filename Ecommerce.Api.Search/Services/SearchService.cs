using Ecommerce.Api.Search.Interfaces;

namespace Ecommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrdersService ordersService;
        private readonly IProductsService productsService;
        private readonly ICustomersService customersService;

        public SearchService(IOrdersService ordersService,IProductsService productsService,ICustomersService customersService)
        {
            this.ordersService = ordersService;
            this.productsService = productsService;
            this.customersService = customersService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
        {
            var result = await ordersService.GetOrdersAsync(customerId);
            var productsResult = await productsService.GetProductsAsync();
            var customerResult = await customersService.GetCustomersAsync();
            if (result.IsSuccess)
            {
                foreach(var order in result.Orders)
                {
                    foreach(var item in order.Items)
                    {
                        item.ProductName = productsResult.IsSuccess? 
                            productsResult.Products.FirstOrDefault(x => x.Id == item.ProductId)?.Name
                            :"Product Info is unavailable";
                    }
                    order.Customer = customerResult.Customers.FirstOrDefault(p => p.Id == customerId);
                }


                var orderResult = new
                {
                    Orders = result.Orders,
                };
                return (true, orderResult);
            }
            return (false, null);
        }
    }
}
