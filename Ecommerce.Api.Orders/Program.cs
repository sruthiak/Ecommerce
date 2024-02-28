using Ecommerce.Api.Orders.Db;
using Ecommerce.Api.Orders.Interfaces;
using Ecommerce.Api.Orders.Providers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//DI
builder.Services.AddScoped<IOrdersProvider, OrdersProvider>();
//add DbContext
builder.Services.AddDbContext<OrdersDbContext>(options => 
{ options.UseInMemoryDatabase("Orders");
    //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
}) ;
//add automapper
builder.Services.AddAutoMapper(typeof(Program));


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
