using ProvaPub.Domain.Interfaces;

namespace ProvaPub.Infrastructure.Payments.Factories
{
    public class PaymentProcessorFactory : IPaymentProcessorFactory
    {
        private readonly Dictionary<string, IPaymentProcessor> _processors;

        public PaymentProcessorFactory()
        {
            _processors = new Dictionary<string, IPaymentProcessor>
            {
                { "pix", new PixPaymentProcessor() },
                { "creditcard", new CreditCardPaymentProcessor() },
                { "paypal", new PayPalPaymentProcessor() },
            };
        }

        public IPaymentProcessor GetProcessor(string paymentMethod)
        {
            if (_processors.TryGetValue(paymentMethod.ToLower(), out var processor))
            {
                return processor;
            }

            throw new NotSupportedException($"Payment method '{paymentMethod}' is not supported.");
        }
    }
}
