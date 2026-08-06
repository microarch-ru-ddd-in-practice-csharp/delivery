using DeliveryApp.Core.Domain.Model.OrderAggegate;
using DeliveryApp.Core.Domain.Model.SharedKernel;
using DeliveryApp.Infrastructure.Adapters.PostgeSQL;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DeliveryApp.IntegrationTests.Repositories;

public class OrderRepositoryShould : IntegrationTestBase
{

    [Fact]
    public async Task CanAddOrder()
    {
        // Arrange
        var orderRepository = new OrderRepository(DbContext);
        var order = new Order(
            Guid.NewGuid(),
            new Location(1, 2),
            new Volume(10)
        );
        
        // Act
        await orderRepository.AddAsync(order);
        var unitOfWork = new UnitOfWork(DbContext);
        await unitOfWork.SaveChangesAsync();

        // Assert
        var savedOrder = await orderRepository.GetByIdAsync(order.Id, CancellationToken.None);
        Assert.NotNull(savedOrder);
        Assert.Equal(order.Id, savedOrder.Id);
        Assert.True(order.Location.IsSameLocation(savedOrder.Location));
        Assert.Equal(order.Volume.Capatity, savedOrder.Volume.Capatity);
    }

    [Fact]
    public async Task CanGetAnyCreatedOrderAsync()
    {
        // Arrange
        var orderRepository = new OrderRepository(DbContext);
        var order = new Order(
            Guid.NewGuid(),
            new Location(1, 2),
            new Volume(10)
        );

        // Act
        await orderRepository.AddAsync(order);
        var unitOfWork = new UnitOfWork(DbContext);
        await unitOfWork.SaveChangesAsync();

        // Assert
        var savedOrder = await orderRepository.GetAnyCreatedOrderAsync(CancellationToken.None);
        Assert.NotNull(savedOrder);
    }

    [Fact]
    public async Task CanGetAssignedOrdersAsync()
    {
        // Arrange
        var orderRepository = new OrderRepository(DbContext);
        var order = new Order(
            Guid.NewGuid(),
            new Location(1, 2),
            new Volume(10)
        );

        // Act
        await orderRepository.AddAsync(order);
        var unitOfWork = new UnitOfWork(DbContext);
        await unitOfWork.SaveChangesAsync();
        order.Assign();
        await orderRepository.UpdateAsync(order);
        await unitOfWork.SaveChangesAsync();


        // Assert
        var assignedOrders = await orderRepository.GetAssignedOrdersAsync(CancellationToken.None);
        Assert.NotEmpty(assignedOrders);
    }
}
