using DeliveryApp.Api.Adapters.Http.Contract.OpenApi.Controllers;
using DeliveryApp.Api.Adapters.Http.Contract.OpenApi.Models;
using DeliveryApp.Core.Application.UseCases.Commands.CompleteOrderCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DeliveryApp.Api.Adapters.Http;

public class CompleteOrderController (IMediator mediator) : CompleteOrderApiController
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    public override async Task<IActionResult> CompleteOrder([FromRoute(Name = "courierId")][Required] Guid courierId, [FromRoute(Name = "orderId")][Required] Guid orderId)
    {
        try
        {
            var response = await _mediator.Send(new CompleteOrderCommand(courierId, orderId));
            if (response) return StatusCode(StatusCodes.Status200OK, new CreateCourierResponse() { CourierId = courierId });

            return BadRequest (new Error(StatusCodes.Status400BadRequest, "Некорректные параметры запроса"));
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Error (ex.Message));
        }
    }
}
