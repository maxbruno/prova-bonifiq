namespace ProvaPub.Services
{
    public class PixPaymentProcessor : IPaymentProcessor
    {
        public string PaymentMethod => "pix";

        public async Task<bool> ProcessPayment(decimal amount, int customerId)
        {
            await Task.Delay(100);
            return true;
        }
    }
}
