using DeliveryApp.Core.Domain.Model.SharedKernel;
using System;
using System.Runtime.CompilerServices;

namespace DeliveryApp.Core.Application.UseCases.Queries.GetNotCompletedOrdersQuery;

public class GetNotCompletedOrdersQueryDto
{

    public GetNotCompletedOrdersQueryDto(
        Guid orderId,
        int locationx,
        int locationy )
    {
        OrderId = orderId;
        LocationX = locationx;
        LocationY = locationy;
    }
    public Guid OrderId { get; private init; }
    
    public int LocationX { get; private init; }

    public int LocationY { get; private init; }
}
