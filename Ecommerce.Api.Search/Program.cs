using Ecommerce.Api.Search.Interfaces;
using Ecommerce.Api.Search.Services;
using Polly;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<ICustomersService, CustomerService>();
builder.Services.AddHttpClient("OrderServices", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:Orders"]);
});
builder.Services.AddHttpClient("ProductServices", configureClient =>
{
    configureClient.BaseAddress =new Uri( builder.Configuration["Services:Products"]);
}).AddTransientHttpErrorPolicy(p=>p.WaitAndRetryAsync(5,_=> TimeSpan.FromMilliseconds(500)));

builder.Services.AddHttpClient("CustomerServices", config =>
{
    config.BaseAddress = new Uri(builder.Configuration["Services:Customers"]);
}).AddTransientHttpErrorPolicy(p=>p.WaitAndRetryAsync(5,_=>TimeSpan.FromMilliseconds(500)));
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
