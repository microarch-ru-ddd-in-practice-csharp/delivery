using Ddd;
using DeliveryApp.Core.Domain.Model.SharedKernel;
using DeliveryApp.Core.Ports;
using MediatR;




namespace DeliveryApp.Core.Application.UseCases.Commands.CompleteOrderCommand;

public class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICourierRepository _courierRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CompleteOrderCommandHandler(IOrderRepository orderRepository, ICourierRepository courierRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _courierRepository = courierRepository ?? throw new ArgumentNullException(nameof(courierRepository));
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public async Task<bool> Handle(CompleteOrderCommand request, CancellationToken cancellationToken)
    {
        var courier = await _courierRepository.GetByIdAsync(request.CourierId, cancellationToken);
        var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order == null || courier == null)
        {
            return false;
        }

        courier.CompliteAssigment(order.Id);
        order.Complete();
        await _courierRepository.UpdateAsync(courier);
        await _orderRepository.UpdateAsync(order);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}
