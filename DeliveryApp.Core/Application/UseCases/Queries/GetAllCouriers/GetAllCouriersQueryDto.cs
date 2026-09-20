using DeliveryApp.Core.Domain.Model.SharedKernel;
using System;

namespace DeliveryApp.Core.Application.UseCases.Queries.GetAllCouriers;

public class GetAllCouriersQueryDto
{
    public Guid CourierId { get; set; }

    public string Name { get; set; }

    public Location Location { get; set; }
}
