namespace ProvaPub.Domain.Interfaces
{
    public interface IPaymentProcessor
    {
        Task<bool> ProcessPayment(decimal amount, int customerId);
        string PaymentMethod { get; }
    }
}
