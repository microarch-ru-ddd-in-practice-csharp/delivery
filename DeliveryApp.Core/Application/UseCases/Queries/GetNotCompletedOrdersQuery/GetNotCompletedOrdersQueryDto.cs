using DeliveryApp.Core.Domain.Model.SharedKernel;
using System;
using System.Runtime.CompilerServices;

namespace DeliveryApp.Core.Application.UseCases.Queries.GetNotCompletedOrdersQuery;

public class GetNotCompletedOrdersQueryDto
{

    public GetNotCompletedOrdersQueryDto(
        Guid orderId,
        Location location )
    {
        OrderId = orderId;
        Location = location;
    }
    public Guid OrderId { get; private init; }
    
    public Location Location { get; private init; }


}
