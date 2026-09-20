using Ddd;
using DeliveryApp.Core.Domain.Model.CourierAggregate;
using DeliveryApp.Core.Domain.Model.SharedKernel;
using DeliveryApp.Core.Ports;
using MediatR;

namespace DeliveryApp.Core.Application.UseCases.Commands.CreateCourierCommand;

public class CreateCourierCommandHandler : IRequestHandler<CreateCourierCommand, CreateResponse>
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly ICourierRepository _courierRepository;

    public CreateCourierCommandHandler (IUnitOfWork unitOfWork, ICourierRepository courierRepository)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _courierRepository = courierRepository ?? throw new ArgumentNullException(nameof(courierRepository));
    }
    public async Task<CreateResponse> Handle(CreateCourierCommand command, CancellationToken cancellationToken)
    {
        var location = new Location(1,1);
        var courier = new Courier(command.Name, location);
        await _courierRepository.AddAsync(courier);
        await _unitOfWork.SaveChangesAsync();
        return new CreateResponse(courier.Id);
    }
}
