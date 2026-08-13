using MediatR;
using System.Collections.Generic;

namespace DeliveryApp.Core.Application.UseCases.Queries.GetAllCouriers;

public class GetAllCouriersQuery : IRequest<IEnumerable<GetAllCouriersQueryDto>>
{
}
