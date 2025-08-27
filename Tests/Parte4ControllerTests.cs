using FluentAssertions;
using ProvaPub.Controllers;
using ProvaPub.Application.Interfaces;
using Moq;
using Xunit;

namespace ProvaPub.Tests
{
    public class Parte4ControllerTests
    {
        private readonly Mock<ICustomerService> _customerServiceMock;
        private readonly Parte4Controller _controller;

        public Parte4ControllerTests()
        {
            _customerServiceMock = new Mock<ICustomerService>();
            _controller = new Parte4Controller(_customerServiceMock.Object);
        }

        [Fact]
        public void Constructor_WithNullCustomerService_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => new Parte4Controller(null!));
            exception.ParamName.Should().Be("customerService");
        }

        [Fact]
        public async Task CanPurchase_WithValidParameters_ShouldCallServiceAndReturnResult()
        {
            // Arrange
            const int customerId = 1;
            const decimal purchaseValue = 75m;
            const bool expectedResult = true;

            _customerServiceMock
                .Setup(x => x.CanPurchase(customerId, purchaseValue))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.CanPurchase(customerId, purchaseValue);

            // Assert
            result.Should().Be(expectedResult);
            _customerServiceMock.Verify(x => x.CanPurchase(customerId, purchaseValue), Times.Once);
        }

        [Fact]
        public async Task CanPurchase_WhenServiceThrowsException_ShouldPropagateException()
        {
            // Arrange
            const int customerId = 1;
            const decimal purchaseValue = 75m;
            var expectedException = new InvalidOperationException("Test exception");

            _customerServiceMock
                .Setup(x => x.CanPurchase(customerId, purchaseValue))
                .ThrowsAsync(expectedException);

            // Act & Assert
            var actualException = await Assert.ThrowsAsync<InvalidOperationException>(
                () => _controller.CanPurchase(customerId, purchaseValue));

            actualException.Should().Be(expectedException);
        }

        [Theory]
        [InlineData(1, 50.5, true)]
        [InlineData(2, 100.0, false)]
        [InlineData(999, 25.75, true)]
        public async Task CanPurchase_WithVariousInputs_ShouldReturnServiceResult(
            int customerId, decimal purchaseValue, bool expectedResult)
        {
            // Arrange
            _customerServiceMock
                .Setup(x => x.CanPurchase(customerId, purchaseValue))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _controller.CanPurchase(customerId, purchaseValue);

            // Assert
            result.Should().Be(expectedResult);
            _customerServiceMock.Verify(x => x.CanPurchase(customerId, purchaseValue), Times.Once);
        }
    }
}
