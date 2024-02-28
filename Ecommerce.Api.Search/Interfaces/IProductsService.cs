﻿using Ecommerce.Api.Search.Models;

namespace Ecommerce.Api.Search.Interfaces
{
    public interface IProductsService
    {
        Task<(bool IsSuccess,IEnumerable<Product> Products,string ErrorMessage)> GetProductsAsync();
    }
}
