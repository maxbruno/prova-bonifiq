namespace ProvaPub.Services
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
