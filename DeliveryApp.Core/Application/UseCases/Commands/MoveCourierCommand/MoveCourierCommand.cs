using DeliveryApp.Core.Domain.Model.SharedKernel;
using MediatR;

namespace DeliveryApp.Core.Application.UseCases.Commands.MoveCourierCommand;

public class MoveCourierCommand : IRequest<bool>
{
    public MoveCourierCommand(Guid courierId, Location location)
    {
        if (courierId == Guid.Empty) throw new ArgumentException("Идентификатор Курьера не может быть пустым");
        CourierId = courierId;
        Location = location;
    }
    public Guid CourierId { get; }
    public Location Location { get; }
    
}
