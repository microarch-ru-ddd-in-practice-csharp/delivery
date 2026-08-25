using MediatR;

namespace DeliveryApp.Core.Application.UseCases.Commands.MoveCouriersCommand;

public class MoveCouriersCommand : IRequest
{
}

public class MoveCouriersCommandHandler : IRequestHandler<MoveCouriersCommand>
{
    public async Task Handle(MoveCouriersCommand request, CancellationToken cancellationToken)
    {
       
    }
}
