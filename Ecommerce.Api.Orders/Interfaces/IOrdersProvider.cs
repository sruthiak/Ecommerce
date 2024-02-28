﻿namespace Ecommerce.Api.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId);
        //Task<(bool IsSuccess, IEnumerable<Models.OrderItem> OrderItems, string ErrorMessage)> GetOrderItemsAsync();

    }
}
