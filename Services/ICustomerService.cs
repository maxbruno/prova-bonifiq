using ProvaPub.Models;

namespace ProvaPub.Services
{
	public interface ICustomerService : IBaseService<Customer>
	{
		PagedList<Customer> ListCustomers(int page);
		Task<bool> CanPurchase(int customerId, decimal purchaseValue);
	}
}
