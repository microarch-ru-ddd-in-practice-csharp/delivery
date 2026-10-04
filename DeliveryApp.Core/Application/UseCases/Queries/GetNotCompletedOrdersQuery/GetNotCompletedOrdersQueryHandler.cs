using DeliveryApp.Core.Ports;
using MediatR;

namespace DeliveryApp.Core.Application.UseCases.Queries.GetNotCompletedOrdersQuery;

public class GetNotCompletedOrdersQueryHandler : IRequestHandler<GetNotCompletedOrdersQuery, IEnumerable<GetNotCompletedOrdersQueryDto>>
{
    private readonly IOrderRepository _orderRepository;

    public GetNotCompletedOrdersQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<IEnumerable<GetNotCompletedOrdersQueryDto>> Handle(GetNotCompletedOrdersQuery request, CancellationToken cancellationToken)
    {
        return await _orderRepository.GetNotCompletedOrdersQueryDtoAsync(cancellationToken);
    }
}
