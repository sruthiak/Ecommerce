using Ecommerce.Api.Products.Db;
using Ecommerce.Api.Products.Interfaces;
using Ecommerce.Api.Products.Providers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//adding DI of IProductsProvider to use the type ProductsProvider
builder.Services.AddScoped<IProductsProvider, ProductsProvider>();

//add automapper
builder.Services.AddAutoMapper(typeof(Program));


//adding DbContext of type ProductsDbContext and configure the data provider as InMemory. Name is Products
builder.Services.AddDbContext<ProductsDbContext>(options =>
{
    options.UseInMemoryDatabase("Products");
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
