namespace ProvaPub.Services
{
    public class PayPalPaymentProcessor : IPaymentProcessor
    {
        public string PaymentMethod => "paypal";

        public async Task<bool> ProcessPayment(decimal amount, int customerId)
        {
            await Task.Delay(150);
            return true;
        }
    }
}
