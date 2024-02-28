using AutoMapper;
using Ecommerce.Api.Orders.Db;
using Ecommerce.Api.Orders.Interfaces;
using Ecommerce.Api.Orders.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrdersDbContext _dbContext;
        private readonly ILogger<OrdersProvider> _logger;
        private readonly IMapper _mapper;
        public OrdersProvider(OrdersDbContext ordersDbContext,ILogger<OrdersProvider> logger,IMapper mapper)
        {
            this._dbContext=ordersDbContext;
            this._mapper=mapper;
            this._logger=logger;
            
            //SeedDataOrderItem();
            SeedDataOrder();
        }

        private void SeedDataOrderItem()
        {
            if (!_dbContext.OrderItems.Any())
            {
                _dbContext.OrderItems.Add(new Db.OrderItem()
                {
                    Id=1,
                    OrderId=1,
                    ProductId=2,
                    UnitPrice=100,
                    Quantity=30
                });
                _dbContext.OrderItems.Add(new Db.OrderItem()
                {
                    Id = 2,
                    OrderId = 1,
                    ProductId = 1,
                    UnitPrice = 50,
                    Quantity = 30
                });
                _dbContext.OrderItems.Add(new Db.OrderItem()
                {
                    Id = 3,
                    OrderId = 2,
                    ProductId = 3,
                    UnitPrice = 80,
                    Quantity = 10
                });
                _dbContext.OrderItems.Add(new Db.OrderItem()
                {
                    Id = 4,
                    OrderId = 2,
                    ProductId = 4,
                    UnitPrice = 100,
                    Quantity = 90
                });
                _dbContext.SaveChanges();
            }
        }

        private  void SeedDataOrder()
        {
            if (!_dbContext.Orders.Any())
            {
                _dbContext.Orders.Add(new Db.Order()
                {
                    CustomerId = 2,
                    Id = 1,
                    OrderDate = Convert.ToDateTime("10-02-2019"),
                    
                    Items =new List<Db.OrderItem>() {
                        new Db.OrderItem(){OrderId=1,ProductId=2,UnitPrice=100,Quantity=30 },
                        new Db.OrderItem(){OrderId = 1,ProductId = 1,UnitPrice = 50,Quantity = 30 }

                    },
                    Total = 200,


                });
                _dbContext.Orders.Add(new Db.Order()
                {
                    CustomerId = 1,
                    Id = 2,
                    OrderDate = Convert.ToDateTime("06-02-2019"),
                    
                    Items = new List<Db.OrderItem>() {
                        new Db.OrderItem(){OrderId=2,ProductId=2,UnitPrice=100,Quantity=30 },
                        new Db.OrderItem(){OrderId = 2,ProductId = 1,UnitPrice = 50,Quantity = 30 }

                    },
                    Total = 200,


                });
                _dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                //Eager load in EF.
                //https://stackoverflow.com/questions/26661771/what-does-include-do-in-linq
                // need to specify include() so that the related entity is loaded. Here Order entity is related to OrderItem.
                // EF by default has lazy load. So the OrderItems will not load. By adding Include(), it is corresponding to JOIN in SQL
                var orders = await _dbContext.Orders.Where(x=>x.CustomerId==customerId).Include(x=>x.Items).ToListAsync();
                if(orders!=null && orders.Any())
                {
                    var result = _mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
                    return (true, result, string.Empty);
                }
                return (false, Enumerable.Empty<Models.Order>(), "Not Found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, Enumerable.Empty<Models.Order>(), ex.Message);

            }
        }

        //public async Task<(bool IsSuccess, IEnumerable<Models.OrderItem> OrderItems, string ErrorMessage)> GetOrderItemsAsync()
        //{
        //    try
        //    {
        //        var orderItems = await _dbContext.OrderItems.ToListAsync();
        //        if (orderItems != null && orderItems.Any())
        //        {
        //            var result = _mapper.Map<IEnumerable<Db.OrderItem>, IEnumerable<Models.OrderItem>>(orderItems);
        //            return (true, result, string.Empty);
        //        }
        //        return (false, Enumerable.Empty<Models.OrderItem>(), "Not Found");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger?.LogError(ex.ToString());
        //        return (false, Enumerable.Empty<Models.OrderItem>(), ex.Message);

        //    }
        //}
    }
}
