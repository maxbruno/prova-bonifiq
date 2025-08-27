using ProvaPub.Domain.Interfaces;

namespace ProvaPub.Domain.Interfaces
{
    public interface IPaymentProcessorFactory
    {
        IPaymentProcessor GetProcessor(string paymentMethod);
    }
}
