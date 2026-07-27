using DeliveryApp.Core.Domain.Model.CounterAggegate;
using DeliveryApp.Core.Domain.Model.OrderAggegate;
using DeliveryApp.Core.Domain.Model.SharedKernel;
using DeliveryApp.Core.Domain.Services.OrderAssignment;
using System;
using System.Collections.Generic;
using Xunit;

namespace DeliveryApp.UnitTests.Domain.Services;

public class OrderAssignmentServiceShould
{
    readonly IOrderAssignmentService _service = new OrderAssignmentService();
    
    [Fact]
    public void ShouldThrowArgumentNullExceptionIfOrderIsNull()
    {
        // Arrange
        Order order = null;
        var couriers = new List<Courier>();

        // Assert
        Assert.Throws<ArgumentNullException>(() => _service.AssignOrderToCourier(order, couriers));
    }

    [Fact]
    public void ShouldThrowNoOrderStatusCreatedExceptionIfOrderIsComplited()
    {
        // Arrange
        var order = new Order(Guid.NewGuid(), new Location(2, 2), new Volume(10));
        order.Assign();
        var couriers = new List<Courier>();

        // Assert
        Assert.Throws<OrderAssignmentService.NoOrderStatusCreatedException>(() => _service.AssignOrderToCourier(order, couriers));
    }

    [Fact]
    public void ShouldThrowNoAvailibaleCourirsFromVolumeException ()
    {
        // Arrange
        var order = new Order(Guid.NewGuid(), new Location(2, 2), new Volume(25));
        var courrier1 = new Courier("Иванов", new Location(5, 5));
        var couriers = new List<Courier>() { courrier1 };

        // Assert
        Assert.Throws<OrderAssignmentService.NoAvailibaleCourirsFromVolumeException>(() => _service.AssignOrderToCourier(order, couriers));
    }

    [Fact]
    public void ShouldReturnNearstCourier ()
    {
        // Arrange
        var order = new Order(Guid.NewGuid(), new Location(2, 2), new Volume(15));
        var courier1 = new Courier("Иванов", new Location(5, 5));
        var courier2 = new Courier("Иванов 2", new Location(10, 10));
        var courier3 = new Courier("Иванов 3", new Location(3, 2));
        var couriers = new List<Courier>() { courier1, courier2, courier3 };

        // Act
        var courier = _service.AssignOrderToCourier (order, couriers);

        // Assert
        Assert.Equal(courier, courier3);
    }

    [Fact]
    public void ShouldReturnNearstCourierExclusiveFull()
    {
        // Arrange
        var order = new Order(Guid.NewGuid(), new Location(2, 2), new Volume(15));
        var courier1 = new Courier("Иванов", new Location(5, 5));
        var courier2 = new Courier("Иванов 2", new Location(10, 10));
        var courier3 = new Courier("Иванов 3", new Location(3, 2));
        var courier4 = new Courier("Иванов 4", new Location(2, 2));
        courier4.AddAssignment(Guid.NewGuid(), new Volume(10), new Location(6, 4));
        var couriers = new List<Courier>() { courier1, courier2, courier3, courier4 };

        // Act
        var courier = _service.AssignOrderToCourier(order, couriers);

        // Assert
        Assert.Equal(courier, courier3);
    }
}
