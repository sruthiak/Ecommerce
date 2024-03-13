using AutoMapper;
using Ecommerce.Api.Products.Db;
using Ecommerce.Api.Products.Profiles;
using Ecommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Products.Tests
{
    public class ProductServiceTest
    {
        [Fact]
        public async Task GetProductsReturnAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnAllProducts))
                .Options;
            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);
            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(productProfile));
            var mapper = new Mapper(configuration);
            var productProvider = new ProductsProvider(dbContext,null,mapper);
            var products=await productProvider.GetProductsAsync();
            Assert.True(products.IsSuccess);
            Assert.True(products.Products.Any());
            //in productprovider class it returns empty error message
            Assert.Empty(products.ErrorMessage);
        }

        [Fact]
        public async Task GetProductlProductUsingValidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductlProductUsingValidId))
                .Options;
            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);
            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(productProfile));
            var mapper = new Mapper(configuration);
            var productProvider = new ProductsProvider(dbContext, null, mapper);
            var products = await productProvider.GetProductAsync(1);
            Assert.True(products.IsSuccess);
            Assert.NotNull(products.Product);
            Assert.True(products.Product.Id == 1);
            //in productprovider class it returns empty error message
            Assert.Empty(products.ErrorMessage);
        }

        [Fact]
        public async Task GetProductlProductUsingInValidId()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductlProductUsingInValidId))
                .Options;
            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);
            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(config => config.AddProfile(productProfile));
            var mapper = new Mapper(configuration);
            var productProvider = new ProductsProvider(dbContext, null, mapper);
            var products = await productProvider.GetProductAsync(100);
            Assert.False(products.IsSuccess);
            Assert.Null(products.Product);
            //in productprovider class it returns empty error message
            Assert.NotNull(products.ErrorMessage);
        }

        private void CreateProducts(ProductsDbContext dbContext)
        {
            for (int i = 1; i < 5; i++)
            {
                dbContext.Products.Add(new Product()
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = i + 10,
                    Price = (decimal) (i * 3.4)
                });
            }
            dbContext.SaveChanges();
        }
    }
}