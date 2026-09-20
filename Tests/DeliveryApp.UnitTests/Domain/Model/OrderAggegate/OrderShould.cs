using DeliveryApp.Core.Domain.Model.OrderAggegate;
using DeliveryApp.Core.Domain.Model.SharedKernel;
using System;
using Xunit;

namespace DeliveryApp.UnitTests.Domain.Model.OrderAggegate
{
    public class OrderShould
    {

        [Fact]
        public void ShouldCreateOrderWithValidParameters()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var location = new Location(1, 5);
            var volume = new Volume(10);

            // Act
            var order = new Order(orderId, location, volume);

            // Assert
            Assert.Equal(orderId, order.Id);
            Assert.Equal(location, order.Location);
            Assert.Equal(volume, order.Volume);
        }


        [Fact]
        public void ShouldThrowArgumentNullExceptionForNullLocation()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var volume = new Volume(10);
            Location location = null;
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Order(orderId, location, volume));
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionForNullVolume()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            Volume volume = null;
            var location = new Location(1, 5);
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new Order(orderId, location, volume));
        }

        [Fact]
        public void ShouldChangeStatusToAssignedFromCreated()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var location = new Location(1, 5);
            var volume = new Volume(10);
            var order = new Order(orderId, location, volume);
            // Act
            order.Assign();
            // Assert
            Assert.Equal(OrderStatus.Assigned, order.Status);
        }

        [Fact]
        public void ShouldThrowOrderInvalidStatusExceptionWhenChangingStatusToComplitedFromNonCreated()
        {
            // Arrange
            var orderId = Guid.NewGuid();
            var location = new Location(1, 5);
            var volume = new Volume(10);
            var order = new Order(orderId, location, volume);

            // Act & Assert
            Assert.Throws<Order.OrderInvalidStatusException>(() => order.Complete());
        }

        
    }
}