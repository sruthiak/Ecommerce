using Ecommerce.Api.Customers.Db;
using Ecommerce.Api.Customers.Interfaces;
using Ecommerce.Api.Customers.Providers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// add DI of ICustomersProvider to CustomersProvider class
builder.Services.AddScoped<ICustomersProvider, CustomersProvider>();

//automapper
builder.Services.AddAutoMapper(typeof(Program));

//add DbContext
builder.Services.AddDbContext<CustomersDbContext>(options =>
{
    options.UseInMemoryDatabase("Customer");
});

//DI
builder.Services.AddScoped<ICustomersProvider, CustomersProvider>();

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
