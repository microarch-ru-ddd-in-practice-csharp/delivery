using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace DeliveryApp.Core.Application.UseCases.Commands.CompleteOrderCommand;

public class CompleteOrderCommand : IRequest<bool>
{
    public CompleteOrderCommand(Guid courierId, Guid orderId)
    {
        CourierId = courierId;
        OrderId = orderId;
    }

    public Guid CourierId { get; }
    public Guid OrderId { get; }
}
