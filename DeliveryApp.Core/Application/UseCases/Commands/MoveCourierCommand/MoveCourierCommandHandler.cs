using Ddd;
using DeliveryApp.Core.Ports;
using MediatR;

namespace DeliveryApp.Core.Application.UseCases.Commands.MoveCourierCommand;

public class MoveCourierCommandHandler : IRequestHandler<MoveCourierCommand, bool>
{
    private readonly ICourierRepository _courierRepository;
    private readonly IUnitOfWork _unitOfWork;

    public MoveCourierCommandHandler(ICourierRepository courierRepository, IUnitOfWork unitOfWork)
    {
        _courierRepository = courierRepository ?? throw new ArgumentNullException(nameof(courierRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<bool> Handle(MoveCourierCommand request, CancellationToken cancellationToken)
    {
        var courier = await _courierRepository.GetByIdAsync(request.CourierId, cancellationToken);
        if (courier == null) throw new Exception("Курьер не найден");
        courier.Move(request.Location);
        await _courierRepository.UpdateAsync(courier);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
