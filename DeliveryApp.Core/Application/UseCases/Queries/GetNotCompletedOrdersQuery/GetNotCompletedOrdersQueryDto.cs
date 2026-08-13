using DeliveryApp.Core.Domain.Model.SharedKernel;
using System;

namespace DeliveryApp.Core.Application.UseCases.Queries.GetNotCompletedOrdersQuery;

public class GetNotCompletedOrdersQueryDto
{
    public Guid OrderId { get; set; }
    public Location Location { get; set; }
}
