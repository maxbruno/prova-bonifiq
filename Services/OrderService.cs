using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
	public class OrderService
	{
        private readonly TestDbContext _ctx;
        private readonly IPaymentProcessorFactory _paymentProcessorFactory;

        public OrderService(TestDbContext ctx, IPaymentProcessorFactory? paymentProcessorFactory = null)
        {
            _ctx = ctx;
            _paymentProcessorFactory = paymentProcessorFactory ?? new PaymentProcessorFactory();
        }

        public async Task<Order> PayOrder(string paymentMethod, decimal paymentValue, int customerId)
		{
            var processor = _paymentProcessorFactory.GetProcessor(paymentMethod);
            var paymentSuccess = await processor.ProcessPayment(paymentValue, customerId);

            if (!paymentSuccess)
            {
                throw new InvalidOperationException("Payment processing failed.");
            }

			return await InsertOrder(new Order() 
            {
                Value = paymentValue,
                CustomerId = customerId,
                OrderDate = DateTime.UtcNow
            });
		}

		public async Task<Order> InsertOrder(Order order)
        {
			var result = await _ctx.Orders.AddAsync(order);
            await _ctx.SaveChangesAsync();
			return result.Entity;
        }
	}
}
