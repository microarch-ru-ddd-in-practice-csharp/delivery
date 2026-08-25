using Ddd;
using DeliveryApp.Core.Application.UseCases.Commands.CreateCourier;
using DeliveryApp.Core.Domain.Model.CourierAggregate;
using DeliveryApp.Core.Ports;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DeliveryApp.UnitTests.Application.Commands;

public class CreateCourierCommandShould
{
    private readonly IUnitOfWork _unitOfWorkMock = Substitute.For<IUnitOfWork>();
    private readonly ICourierRepository _courierRepositoryMock = Substitute.For<ICourierRepository>();

    [Fact]
    public async Task ShouldCreateCourer()
    {
        // Arrange
        var command = new CreateCourierCommand("Иван Иванович");
        _unitOfWorkMock.SaveChangesAsync().Returns(Task.FromResult(true));
        _courierRepositoryMock.AddAsync(Arg.Any<Courier>()).Returns(Task.FromResult(true));
        var handler = new CreateCourierCommandHandler(_unitOfWorkMock, _courierRepositoryMock);

        // Act
        var result = await handler.Handle(command, default);

        // Assert
        Assert.True(result.Ok);
        await _courierRepositoryMock.Received(1).AddAsync(Arg.Any<Courier>());
        await _unitOfWorkMock.Received(1).SaveChangesAsync();
    }
}
