using Ddd;
using DeliveryApp.Core.Application.UseCases.Commands.CreateOrder;
using DeliveryApp.Core.Domain.Model.OrderAggegate;
using DeliveryApp.Core.Ports;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DeliveryApp.UnitTests.Application.Commands;

public class CreateOrderCommandShould
{
    private readonly IUnitOfWork _unitOfWorkMock = Substitute.For<IUnitOfWork>();
    private readonly IOrderRepository _orderRepositoryMock = Substitute.For<IOrderRepository>();

    [Fact]
    public async Task ShouldCreateOrder()
    {
        // Arrange
        var command = new CreateOrderCommand(Guid.NewGuid(), "Россия", "Москва", "Красная площадь", "34", "120", 20);
        _unitOfWorkMock.SaveChangesAsync().Returns(Task.FromResult(true));
        _orderRepositoryMock.AddAsync(Arg.Any<Order>()).Returns(Task.FromResult(true));
        var handler = new CreateOrderCommandHandler(_orderRepositoryMock, _unitOfWorkMock);

        // Act
        var result = await handler.Handle(command, default);
        // Assert
        Assert.True(result);
        await _orderRepositoryMock.Received(1).AddAsync(Arg.Any<Order>());
        await _unitOfWorkMock.Received(1).SaveChangesAsync();
    }
}
