using DeliveryApp.Api.Adapters.Http.Contract.OpenApi.Controllers;
using DeliveryApp.Api.Adapters.Http.Contract.OpenApi.Models;
using DeliveryApp.Core.Application.UseCases.Commands.CompleteOrderCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static DeliveryApp.Core.Domain.Model.OrderAggegate.Order;

namespace DeliveryApp.Api.Adapters.Http;

public class CompleteOrderController (IMediator mediator) : CompleteOrderApiController
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    public override async Task<IActionResult> CompleteOrder([FromRoute(Name = "courierId")][Required] Guid courierId, [FromRoute(Name = "orderId")][Required] Guid orderId)
    {
        try
        {
            var response = await _mediator.Send(new CompleteOrderCommand(courierId, orderId));
            if (response) return Ok();

            return BadRequest (new Error(StatusCodes.Status400BadRequest, "Некорректные параметры запроса"));
        }
        catch(OrderInvalidStatusException ex)
        {
            return StatusCode(StatusCodes.Status409Conflict, new Error(ex.Message));
        }
        catch(ArgumentException ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new Error(ex.Message));
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Error (ex.Message));
        }
    }
}
