using Ddd;
using DeliveryApp.Core.Application.UseCases.Commands.CompleteOrderCommand;
using DeliveryApp.Core.Domain.Model.CourierAggregate;
using DeliveryApp.Core.Domain.Model.OrderAggegate;
using DeliveryApp.Core.Domain.Model.SharedKernel;
using DeliveryApp.Core.Ports;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DeliveryApp.UnitTests.Application.Commands;

public class CompleteOrderCommandShould
{
    private readonly IUnitOfWork _unitOfWorkMock = Substitute.For<IUnitOfWork>();
    private readonly IOrderRepository _orderRepositoryMock = Substitute.For<IOrderRepository>();
    private readonly ICourierRepository _courierRepositoryMock = Substitute.For<ICourierRepository>();

    [Fact]
    public async Task ShouldCompleteOrderCommand()
    {

        // Arrange
        var courier = new Courier("Курьер 1", new Location (5, 4));
        var order = new Order(Guid.NewGuid(), new Location(5, 7), new Volume(10));
        courier.AddAssignment(order);
        order.Assign();

        courier.Move(new Location(5, 5));
        courier.Move(new Location(5, 6));
        _unitOfWorkMock.SaveChangesAsync().Returns(Task.FromResult(true));
        _orderRepositoryMock.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(Task.FromResult(order));
        _courierRepositoryMock.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(Task.FromResult(courier));
        

        var command = new CompleteOrderCommand(courier.Id, order.Id);
        var handler = new CompleteOrderCommandHandler(_orderRepositoryMock, _courierRepositoryMock, _unitOfWorkMock);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        Assert.True(result);
        Assert.Equal(OrderStatus.Completed, order.Status);
        Assert.Equal(courier.Assignments[0].Status, AssignmentStatus.Completed);
        await _orderRepositoryMock.Received(1).GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
        await _courierRepositoryMock.Received(1).GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
        await _unitOfWorkMock.Received(1).SaveChangesAsync();

    }
}
