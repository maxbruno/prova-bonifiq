using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;
using ProvaPub.Services;
using Xunit;

namespace ProvaPub.Tests
{
    public class CustomerServiceTests : IDisposable
    {
        private readonly TestDbContext _context;
        private readonly CustomerService _customerService;

        public CustomerServiceTests()
        {
            var options = new DbContextOptionsBuilder<TestDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new TestDbContext(options);
            _customerService = new CustomerService(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        #region Input Validation Tests

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public async Task CanPurchase_WithInvalidCustomerId_ShouldThrowArgumentOutOfRangeException(int invalidCustomerId)
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
                () => _customerService.CanPurchase(invalidCustomerId, 50m));

            exception.ParamName.Should().Be("customerId");
            exception.Message.Should().Contain("Customer ID must be greater than zero");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-100)]
        public async Task CanPurchase_WithInvalidPurchaseValue_ShouldThrowArgumentOutOfRangeException(decimal invalidPurchaseValue)
        {
            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentOutOfRangeException>(
                () => _customerService.CanPurchase(1, invalidPurchaseValue));

            exception.ParamName.Should().Be("purchaseValue");
            exception.Message.Should().Contain("Purchase value must be greater than zero");
        }

        #endregion

        #region Customer Existence Tests

        [Fact]
        public async Task CanPurchase_WithNonExistentCustomer_ShouldThrowInvalidOperationException()
        {
            // Arrange
            const int nonExistentCustomerId = 999;

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(
                () => _customerService.CanPurchase(nonExistentCustomerId, 50m));

            exception.Message.Should().Contain($"Customer with ID {nonExistentCustomerId} does not exist");
        }

        #endregion

        #region Monthly Purchase Limit Tests

        [Fact]
        public async Task CanPurchase_WhenCustomerAlreadyPurchasedThisMonth_ShouldReturnFalse()
        {
            // Arrange
            var customer = new Customer { Id = 1, Name = "Test Customer" };
            var existingOrder = new Order
            {
                Id = 1,
                CustomerId = 1,
                Value = 50m,
                OrderDate = DateTime.UtcNow.AddDays(-15)
            };

            _context.Customers.Add(customer);
            _context.Orders.Add(existingOrder);
            await _context.SaveChangesAsync();

            // Act
            var result = await _customerService.CanPurchase(1, 75m);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task CanPurchase_WhenCustomerPurchasedMoreThanAMonthAgo_ShouldReturnTrue()
        {
            // Arrange
            var customer = new Customer { Id = 1, Name = "Test Customer" };
            var oldOrder = new Order
            {
                Id = 1,
                CustomerId = 1,
                Value = 50m,
                OrderDate = DateTime.UtcNow.AddMonths(-2)
            };

            _context.Customers.Add(customer);
            _context.Orders.Add(oldOrder);
            await _context.SaveChangesAsync();

            // Act
            var result = await _customerService.CanPurchase(1, 75m);

            // Assert
            if (IsCurrentlyBusinessHours())
            {
                result.Should().BeTrue();
            }
            else
            {
                result.Should().BeFalse();
            }
        }

        #endregion

        #region First Time Purchase Limit Tests

        [Fact]
        public async Task CanPurchase_FirstTimePurchaseOver100_ShouldReturnFalse()
        {
            // Arrange
            var customer = new Customer { Id = 1, Name = "Test Customer" };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            // Act
            var result = await _customerService.CanPurchase(1, 150m);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task CanPurchase_FirstTimePurchaseExactly100_ShouldReturnTrue()
        {
            // Arrange
            var customer = new Customer { Id = 1, Name = "Test Customer" };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            // Act
            var result = await _customerService.CanPurchase(1, 100m);

            // Assert
            if (IsCurrentlyBusinessHours())
            {
                result.Should().BeTrue();
            }
            else
            {
                result.Should().BeFalse();
            }
        }

        [Fact]
        public async Task CanPurchase_FirstTimePurchaseUnder100_ShouldReturnTrue()
        {
            // Arrange
            var customer = new Customer { Id = 1, Name = "Test Customer" };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            // Act
            var result = await _customerService.CanPurchase(1, 75m);

            // Assert
            if (IsCurrentlyBusinessHours())
            {
                result.Should().BeTrue();
            }
            else
            {
                result.Should().BeFalse();
            }
        }

        [Fact]
        public async Task CanPurchase_ReturningCustomerPurchaseOver100_ShouldReturnTrue()
        {
            // Arrange
            var customer = new Customer { Id = 1, Name = "Test Customer" };
            var oldOrder = new Order
            {
                Id = 1,
                CustomerId = 1,
                Value = 50m,
                OrderDate = DateTime.UtcNow.AddMonths(-2)
            };

            _context.Customers.Add(customer);
            _context.Orders.Add(oldOrder);
            await _context.SaveChangesAsync();

            // Act
            var result = await _customerService.CanPurchase(1, 150m);

            // Assert
            if (IsCurrentlyBusinessHours())
            {
                result.Should().BeTrue();
            }
            else
            {
                result.Should().BeFalse();
            }
        }

        #endregion

        #region Integration Tests

        [Fact]
        public async Task CanPurchase_ValidScenario_ShouldReturnCorrectResult()
        {
            // Arrange
            var customer = new Customer { Id = 1, Name = "Test Customer" };
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            // Act
            var result = await _customerService.CanPurchase(1, 75m);

            // Assert
            if (IsCurrentlyBusinessHours())
            {
                result.Should().BeTrue();
            }
            else
            {
                result.Should().BeFalse();
            }
        }

        #endregion

        private static bool IsCurrentlyBusinessHours()
        {
            var now = DateTime.UtcNow;
            var isWeekend = now.DayOfWeek == DayOfWeek.Saturday || 
                           now.DayOfWeek == DayOfWeek.Sunday;
            var isOutsideBusinessHours = now.Hour < 8 || now.Hour > 18;

            return !isWeekend && !isOutsideBusinessHours;
        }
    }
}
