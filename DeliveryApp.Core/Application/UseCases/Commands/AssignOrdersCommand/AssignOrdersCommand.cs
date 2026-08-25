using Ddd;
using DeliveryApp.Core.Domain.Services.OrderAssignment;
using DeliveryApp.Core.Ports;
using MediatR;

namespace DeliveryApp.Core.Application.UseCases.Commands.AssignOrdersCommand;

public class AssignOrdersCommand : IRequest
{
}

public class AssignOrdersCommandHandler : IRequestHandler<AssignOrdersCommand>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICourierRepository _courierRepository;

    private readonly IOrderAssignmentService _orderAssignmentService;

    private readonly IUnitOfWork _unitOfWork;

    public AssignOrdersCommandHandler(
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

    public async Task Handle(AssignOrdersCommand request, CancellationToken cancellationToken)
    {
        var newOrders = await _orderRepository.GetCreatedOrdersAsync(cancellationToken);
        foreach (var order in newOrders)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var couriers = await _courierRepository.GetAllAsync(cancellationToken);
            _orderAssignmentService.AssignOrderToCourier(order, couriers.ToList());
        }
        await _unitOfWork.SaveChangesAsync();
    }
}