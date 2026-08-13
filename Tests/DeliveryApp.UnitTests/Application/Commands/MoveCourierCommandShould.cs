using Ddd;
using DeliveryApp.Core.Ports;
using NSubstitute;
using System;
using Xunit;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DeliveryApp.Core.Application.UseCases.Commands.MoveCourierCommand;
using DeliveryApp.Core.Domain.Model.CounterAggegate;
using DeliveryApp.Core.Domain.Model.SharedKernel;
using System.Threading;

namespace DeliveryApp.UnitTests.Application.Commands;

public class MoveCourierCommandShould
{
    private readonly IUnitOfWork _unitOfWorkMock = Substitute.For<IUnitOfWork>();
    private readonly ICourierRepository _courierRepositoryMock = Substitute.For<ICourierRepository>();

    [Fact]
    public async Task ShouldMoveCourier()
    {
        // Arrange
        var courier = new Courier("John Doe", new Location(4,3));
        var location = new Location(5, 3);       
        var command = new MoveCourierCommand(Guid.NewGuid(), location);

        _unitOfWorkMock.SaveChangesAsync().Returns(Task.FromResult(true));
        _courierRepositoryMock.GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(Task.FromResult(courier));
        _courierRepositoryMock.UpdateAsync(Arg.Any<Courier>()).Returns(Task.FromResult(true));
        var handler = new MoveCourierCommandHandler(_courierRepositoryMock, _unitOfWorkMock);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        Assert.True(result);
        await _courierRepositoryMock.Received(1).GetByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());   
        await _courierRepositoryMock.Received(1).UpdateAsync(Arg.Any<Courier>());
        await _unitOfWorkMock.Received(1).SaveChangesAsync();
    }
}
