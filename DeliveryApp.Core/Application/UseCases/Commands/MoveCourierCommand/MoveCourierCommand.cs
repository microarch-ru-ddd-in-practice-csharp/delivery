using DeliveryApp.Core.Domain.Model.SharedKernel;
using MediatR;

namespace DeliveryApp.Core.Application.UseCases.Commands.MoveCourierCommand;

public class MoveCourierCommand : IRequest<bool>
{
    public MoveCourierCommand(Guid courierId, Location location)
    {
        CourierId = courierId;
        Location = location;
    }
    public Guid CourierId { get; }
    public Location Location { get; }
    
}
