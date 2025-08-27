namespace ProvaPub.Domain.Entities
{
    public class OrderDto
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; }

        public static OrderDto FromOrder(Order order)
        {
            return new OrderDto
            {
                Id = order.Id,
                Value = order.Value,
                CustomerId = order.CustomerId,
                Customer = order.Customer,
                OrderDate = TimeZoneInfo.ConvertTimeFromUtc(order.OrderDate,
                    TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"))
            };
        }
    }
}
