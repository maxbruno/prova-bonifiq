using ProvaPub.Domain.Interfaces;

namespace ProvaPub.Infrastructure.Payments.Strategies
{
    public class CreditCardPaymentProcessor : IPaymentProcessor
    {
        public string PaymentMethod => "creditcard";

        public async Task<bool> ProcessPayment(decimal amount, int customerId)
        {
            await Task.Delay(200);
            return true;
        }
    }
}
