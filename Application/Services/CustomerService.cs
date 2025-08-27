using Microsoft.EntityFrameworkCore;
using ProvaPub.Domain.Entities;
using ProvaPub.Domain.Services;
using ProvaPub.Repository;
using ProvaPub.Application.Interfaces;

namespace ProvaPub.Application.Services
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        private readonly CustomerDomainService _domainService;

        public CustomerService(TestDbContext ctx, CustomerDomainService domainService) : base(ctx)
        {
            _domainService = domainService ?? throw new ArgumentNullException(nameof(domainService));
        }

        protected override DbSet<Customer> GetDbSet()
        {
            return _ctx.Customers;
        }

        public PagedList<Customer> ListCustomers(int page)
        {
            return GetPagedList(page);
        }

        public async Task<bool> CanPurchase(int customerId, decimal purchaseValue)
        {
            _domainService.ValidatePurchaseRequest(customerId, purchaseValue);

            var customer = await GetCustomerAsync(customerId);
            _domainService.ValidateCustomerExists(customer, customerId);

            var ordersThisMonth = await GetOrdersThisMonthAsync(customerId);
            if (_domainService.HasPurchasedThisMonth(ordersThisMonth))
                return false;

            var hasOrdersBefore = await HasOrdersBeforeAsync(customerId);
            if (_domainService.IsFirstTimePurchase(hasOrdersBefore) && !_domainService.CanMakeFirstTimePurchase(purchaseValue))
                return false;

            if (!_domainService.IsWithinBusinessHours(DateTime.UtcNow))
                return false;

            return true;
        }

        private async Task<Customer?> GetCustomerAsync(int customerId)
        {
            return await _ctx.Customers.FindAsync(customerId);
        }

        private async Task<IEnumerable<Order>> GetOrdersThisMonthAsync(int customerId)
        {
            var oneMonthAgo = DateTime.UtcNow.AddMonths(-1);
            return await _ctx.Orders
                .Where(order => order.CustomerId == customerId && order.OrderDate >= oneMonthAgo)
                .ToListAsync();
        }

        private async Task<bool> HasOrdersBeforeAsync(int customerId)
        {
            return await _ctx.Orders
                .AnyAsync(order => order.CustomerId == customerId);
        }
    }
}
