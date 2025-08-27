using ProvaPub.Domain.Entities;

namespace ProvaPub.Application.Interfaces
{
    public interface ICustomerService : IBaseService<Customer>
    {
        PagedList<Customer> ListCustomers(int page);
        Task<bool> CanPurchase(int customerId, decimal purchaseValue);
    }
}
