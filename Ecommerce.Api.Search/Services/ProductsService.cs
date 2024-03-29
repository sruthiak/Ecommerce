﻿using Ecommerce.Api.Search.Interfaces;
using Ecommerce.Api.Search.Models;
using System.Text.Json;

namespace Ecommerce.Api.Search.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ILogger<ProductsService> logger;

        public ProductsService(IHttpClientFactory httpClientFactory,ILogger<ProductsService> logger)
        {
            this.httpClientFactory = httpClientFactory;
            this.logger = logger;
        }
        public async Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var client = httpClientFactory.CreateClient("ProductServices");
                var response = await client.GetAsync($"/api/products");
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    var options = new JsonSerializerOptions{ PropertyNameCaseInsensitive = true};
                    var result=JsonSerializer.Deserialize<IEnumerable<Product>>(content,options);
                    return (true, result, string.Empty);
                }
                return (false, null, response.ReasonPhrase);
            }
            catch(Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
