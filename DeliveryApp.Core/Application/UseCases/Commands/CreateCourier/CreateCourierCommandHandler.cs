using Ddd;
using DeliveryApp.Core.Domain.Model.CounterAggegate;
using DeliveryApp.Core.Domain.Model.SharedKernel;
using DeliveryApp.Core.Ports;
using MediatR;

namespace DeliveryApp.Core.Application.UseCases.Commands.CreateCourier;

public class CreateCourierCommandHandler : IRequestHandler<CreateCourierCommand, bool>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourierRepository _courierRepository;

    public CreateCourierCommandHandler (IUnitOfWork unitOfWork, ICourierRepository courierRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _courierRepository = courierRepository ?? throw new ArgumentNullException(nameof(courierRepository));
    }
    public async Task<bool> Handle(CreateCourierCommand command, CancellationToken cancellationToken)
    {
        var location = new Location(1,1);
        var courier = new Courier(command.Name, location);
        await _courierRepository.AddAsync(courier);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
