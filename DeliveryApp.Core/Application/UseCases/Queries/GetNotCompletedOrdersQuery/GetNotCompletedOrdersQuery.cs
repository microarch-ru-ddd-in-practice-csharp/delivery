using MediatR;

namespace DeliveryApp.Core.Application.UseCases.Queries.GetNotCompletedOrdersQuery;

public class GetNotCompletedOrdersQuery : IRequest<IEnumerable<GetNotCompletedOrdersQueryDto>>
{
}
