using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace DeliveryApp.Core.Application.UseCases.Commands.CompleteOrderCommand;

public class CompleteOrderCommand : IRequest<bool>
{
    public CompleteOrderCommand(Guid courierId, Guid orderId)
    {
        if (orderId == Guid.Empty) throw new ArgumentException("Идентификатор заказа не может быть пустым");
        if (courierId == Guid.Empty) throw new ArgumentException("Идентификатор Курьера не может быть пустым");
        CourierId = courierId;
        OrderId = orderId;
    }

    public Guid CourierId { get; }
    public Guid OrderId { get; }
}
