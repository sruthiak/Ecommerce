
using AutoMapper;
using Ecommerce.Api.Customers.Db;
using Ecommerce.Api.Customers.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomersDbContext _customersDbContext;
        private readonly ILogger<CustomersProvider> _logger;
        private readonly IMapper _mapper;
        public CustomersProvider(CustomersDbContext customersDbContext,ILogger<CustomersProvider> logger,IMapper mapper)
        {

            this._customersDbContext = customersDbContext;
            this._logger = logger;
            this._mapper = mapper;
            SeedData();

        }

        private void SeedData()
        {
            if (!_customersDbContext.Customers.Any())
            {
                _customersDbContext.Customers.Add(new Db.Customer()
                {
                    Id = 1,
                    Address = "NL",
                    Name = "Arun"
                });
                _customersDbContext.Customers.Add(new Db.Customer()
                {
                    Id = 2,
                    Address = "India",
                    Name = "Hari"
                });
                _customersDbContext.Customers.Add(new Db.Customer()
                {
                    Id = 3,
                    Address = "US",
                    Name = "Joe"
                });
                _customersDbContext.Customers.Add(new Db.Customer()
                {
                    Id = 4,
                    Address = "UK",
                    Name = "James"
                });
                _customersDbContext.SaveChangesAsync();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Customer> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                var customers = await _customersDbContext.Customers.ToListAsync();
                if(customers != null && customers.Any())
                {
                    var result = _mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Models.Customer>>(customers);
                    return (true, result, string.Empty);
                }
                return (false, Enumerable.Empty<Models.Customer>(), "Not Found");
            }
            catch(Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, Enumerable.Empty<Models.Customer>(), ex.Message);
            }
        }

        public async Task<(bool IsSuccess, Models.Customer? Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var customer = await _customersDbContext.Customers.FirstOrDefaultAsync(p=>p.Id==id);
                if (customer != null )
                {
                    var result = _mapper.Map<Db.Customer, Models.Customer>(customer);
                    return (true, result, string.Empty);
                }
                return (false, null, "Not Found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false,null, ex.Message);
            }
        }
    }
}
