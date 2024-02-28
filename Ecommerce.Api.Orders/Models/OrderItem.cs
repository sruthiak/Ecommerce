namespace Ecommerce.Api.Orders.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        //orderId n ot required in model class. only needed in DB class
        //public int OrderId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
