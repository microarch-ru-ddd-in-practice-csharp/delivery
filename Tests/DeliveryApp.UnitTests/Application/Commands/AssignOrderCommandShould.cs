using Ddd;
using DeliveryApp.Core.Application.UseCases.Commands.AssignOrderCommand;
using DeliveryApp.Core.Domain.Model.CourierAggregate;
using DeliveryApp.Core.Domain.Model.OrderAggegate;
using DeliveryApp.Core.Domain.Model.SharedKernel;
using DeliveryApp.Core.Domain.Services.OrderAssignment;
using DeliveryApp.Core.Ports;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace DeliveryApp.UnitTests.Application.Commands;

public class AssignOrderCommandShould
{
    private readonly IUnitOfWork _unitOfWorkMock = Substitute.For<IUnitOfWork>();
    private readonly IOrderRepository _orderRepositoryMock = Substitute.For<IOrderRepository>();
    private readonly ICourierRepository _courierRepositoryMock = Substitute.For<ICourierRepository>();

    private readonly OrderAssignmentService _orderAssignmentService = new OrderAssignmentService();

    [Fact]
    public async Task ShouldAssignOrderCommand()
    {         
        // Arrange
        var command = new AssignOrderCommand();

        var order = new Order(Guid.NewGuid(), new Location(3, 3), new Volume(5));
        var courers = new List<Courier>()
        {
            new Courier("Курьер 1", new Location(4, 3)),
            new Courier("Курьер 2", new Location(4, 7)),
        };

        _unitOfWorkMock.SaveChangesAsync().Returns(Task.FromResult(true));
        _orderRepositoryMock.GetAnyCreatedOrderAsync(Arg.Any<CancellationToken>()).Returns(Task.FromResult(order));
        _courierRepositoryMock.GetAllAsync(Arg.Any<CancellationToken>()).Returns(Task.FromResult(courers.AsEnumerable()));
        
        var handler = new AssignOrderCommandHandler(_orderAssignmentService, _orderRepositoryMock, _courierRepositoryMock, _unitOfWorkMock);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        Assert.True(result);
        Assert.Equal(OrderStatus.Assigned, order.Status);
        Assert.Single(courers[0].Assignments);
        await _orderRepositoryMock.Received(1).UpdateAsync(Arg.Any<Order>());
        await _courierRepositoryMock.Received(1).GetAllAsync(Arg.Any<CancellationToken>());
        await _courierRepositoryMock.Received(1).UpdateAsync(Arg.Any<Courier>());
        await _unitOfWorkMock.Received(1).SaveChangesAsync();


    }
}
