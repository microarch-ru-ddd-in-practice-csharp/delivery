using Ddd;
using DeliveryApp.Core.Ports;
using MediatR;

namespace DeliveryApp.Core.Application.UseCases.Commands.AssignOrderCommand;

public class AssignOrderCommandHandler : IRequestHandler<AssignOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICourierRepository _courierRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AssignOrderCommandHandler(IOrderRepository orderRepository, ICourierRepository courierRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _courierRepository = courierRepository ?? throw new ArgumentNullException(nameof(courierRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<bool> Handle(AssignOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetAnyCreatedOrderAsync(cancellationToken);
        var couriers = await _courierRepository.GetAllAsync(cancellationToken);
        var courier = couriers.FirstOrDefault(c => c.CanAddAssignment(order.Volume));
        if (courier == null)  return false;
        courier.AddAssignment(order);
        order.Assign();
        await _courierRepository.UpdateAsync (courier);
        await _orderRepository.UpdateAsync(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return true;
    }
}
