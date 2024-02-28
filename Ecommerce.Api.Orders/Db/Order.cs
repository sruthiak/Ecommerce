﻿namespace Ecommerce.Api.Orders.Db
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public decimal Total { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderItem> Items { get; set; }
    }
}
