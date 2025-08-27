using ProvaPub.Domain.Entities;

namespace ProvaPub.Domain.Services
{
    public class CustomerDomainService
    {
        public bool CanMakeFirstTimePurchase(decimal purchaseValue)
        {
            return purchaseValue <= 100;
        }

        public bool IsWithinBusinessHours(DateTime currentTime)
        {
            var isWeekend = currentTime.DayOfWeek == DayOfWeek.Saturday ||
                           currentTime.DayOfWeek == DayOfWeek.Sunday;
            var isOutsideBusinessHours = currentTime.Hour < 8 || currentTime.Hour > 18;

            return !isWeekend && !isOutsideBusinessHours;
        }

        public bool HasExceededMonthlyLimit(IEnumerable<Order> ordersThisMonth)
        {
            return ordersThisMonth.Any();
        }

        public bool HasPurchasedThisMonth(IEnumerable<Order> ordersThisMonth)
        {
            return HasExceededMonthlyLimit(ordersThisMonth);
        }

        public bool IsFirstTimePurchase(bool hasOrdersBefore)
        {
            return !hasOrdersBefore;
        }

        public void ValidatePurchaseRequest(int customerId, decimal purchaseValue)
        {
            if (customerId <= 0)
                throw new ArgumentOutOfRangeException(nameof(customerId), "Customer ID must be greater than zero");

            if (purchaseValue <= 0)
                throw new ArgumentOutOfRangeException(nameof(purchaseValue), "Purchase value must be greater than zero");
        }

        public void ValidateCustomerExists(Customer? customer, int customerId)
        {
            if (customer == null)
                throw new InvalidOperationException($"Customer with ID {customerId} does not exist");
        }
    }
}
