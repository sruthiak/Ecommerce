using Ecommerce.Api.Products.Models;

namespace Ecommerce.Api.Products.Interfaces
{
    //Controller calls these methods
    public interface IProductsProvider
    {
        //get all products
        Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync();
        Task<(bool IsSuccess, Product? Product, string ErrorMessage)> GetProductAsync(int id);
    }
}
