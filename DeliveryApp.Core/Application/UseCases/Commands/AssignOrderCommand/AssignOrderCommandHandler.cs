using Ddd;
using DeliveryApp.Core.Domain.Services.OrderAssignment;
using DeliveryApp.Core.Ports;
using MediatR;

namespace DeliveryApp.Core.Application.UseCases.Commands.AssignOrderCommand;

public class AssignOrderCommandHandler : IRequestHandler<AssignOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICourierRepository _courierRepository;

    private readonly IOrderAssignmentService _orderAssignmentService;
    private readonly IUnitOfWork _unitOfWork;

    public AssignOrderCommandHandler(
        IOrderAssignmentService orderAssignmentService,
        IOrderRepository orderRepository, 
        ICourierRepository courierRepository, 
        IUnitOfWork unitOfWork)
    {
        _orderAssignmentService = orderAssignmentService;
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _courierRepository = courierRepository ?? throw new ArgumentNullException(nameof(courierRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<bool> Handle(AssignOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetAnyCreatedOrderAsync(cancellationToken);
        var couriers = await _courierRepository.GetAllAsync(cancellationToken);
        var result = _orderAssignmentService.AssignOrderToCourier(order, couriers.ToList());
        if (result.Successful)
        {
            await _courierRepository.UpdateAsync(result.Courier);
            await _orderRepository.UpdateAsync(order);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }

        return false;
    }
}
