using AutoMapper;
using Ecommerce.Api.Products.Db;
using Ecommerce.Api.Products.Interfaces;
using Ecommerce.Api.Products.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider

    {

        private readonly ProductsDbContext productsDbContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;

        public ProductsProvider(ProductsDbContext productsDbContext,ILogger<ProductsProvider> logger,IMapper mapper)
        {
            this.productsDbContext = productsDbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
            
        }
        //Sample data because we are not connecting with Relational db

        private void SeedData()
        {
            if(!productsDbContext.Products.Any())
            {
                productsDbContext.Products.Add(new Db.Product
                {
                    Id=1,
                    Name="Keyboard",
                    Price=20,
                    Inventory=100

                });
                productsDbContext.Products.Add(new Db.Product
                {
                    Id = 2,
                    Name = "Mouse",
                    Price = 20,
                    Inventory = 50

                });
                productsDbContext.Products.Add(new Db.Product
                {
                    Id = 3,
                    Name = "Monitor",
                    Price = 200,
                    Inventory = 100

                });
                productsDbContext.Products.Add(new Db.Product
                {
                    Id = 4,
                    Name = "CPU",
                    Price = 200,
                    Inventory = 60

                });
                productsDbContext.SaveChangesAsync();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Product> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await productsDbContext.Products.ToListAsync();
                if(products!=null && products.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Product>,IEnumerable<Models.Product>>(products);
                    return(true, result,string.Empty);
                }
                return (false, Enumerable.Empty<Models.Product>(), "Not Found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, Enumerable.Empty<Models.Product>(), ex.Message); 
            }
            
        }

        public async Task<(bool IsSuccess, Models.Product? Product, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                var product = await productsDbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                {
                    var result = mapper.Map<Db.Product, Models.Product>(product);
                    return (true, result, string.Empty);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
