using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        public CustomerService(TestDbContext ctx) : base(ctx)
        {
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
            // Input validation
            if (customerId <= 0) 
                throw new ArgumentOutOfRangeException(nameof(customerId), "Customer ID must be greater than zero");

            if (purchaseValue <= 0) 
                throw new ArgumentOutOfRangeException(nameof(purchaseValue), "Purchase value must be greater than zero");

            // Business Rule: Non registered customers cannot purchase
            var customer = await GetCustomerAsync(customerId);
            if (customer == null) 
                throw new InvalidOperationException($"Customer with ID {customerId} does not exist");

            // Business Rule: A customer can purchase only once per month
            if (await HasPurchasedThisMonthAsync(customerId))
                return false;

            // Business Rule: First-time customers can make a maximum purchase of 100.00
            if (await IsFirstTimePurchaseAsync(customerId) && purchaseValue > 100)
                return false;

            // Business Rule: Purchases are only allowed during business hours and working days
            if (!IsWithinBusinessHours())
                return false;

            return true;
        }

        private async Task<Customer?> GetCustomerAsync(int customerId)
        {
            return await _ctx.Customers.FindAsync(customerId);
        }

        private async Task<bool> HasPurchasedThisMonthAsync(int customerId)
        {
            var oneMonthAgo = DateTime.UtcNow.AddMonths(-1);
            var ordersThisMonth = await _ctx.Orders
                .CountAsync(order => order.CustomerId == customerId && order.OrderDate >= oneMonthAgo);
            
            return ordersThisMonth > 0;
        }

        private async Task<bool> IsFirstTimePurchaseAsync(int customerId)
        {
            var hasOrdersBefore = await _ctx.Orders
                .AnyAsync(order => order.CustomerId == customerId);
            
            return !hasOrdersBefore;
        }

        private bool IsWithinBusinessHours()
        {
            var currentTime = DateTime.UtcNow;
            var isWeekend = currentTime.DayOfWeek == DayOfWeek.Saturday || 
                           currentTime.DayOfWeek == DayOfWeek.Sunday;
            var isOutsideBusinessHours = currentTime.Hour < 8 || currentTime.Hour > 18;

            return !isWeekend && !isOutsideBusinessHours;
        }
    }
}
