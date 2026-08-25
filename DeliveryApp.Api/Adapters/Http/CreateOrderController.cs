using DeliveryApp.Api.Adapters.Http.Contract.OpenApi.Controllers;
using DeliveryApp.Api.Adapters.Http.Contract.OpenApi.Models;
using DeliveryApp.Core.Application.UseCases.Commands.CreateOrder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Api.Adapters.Http;

public class CreateOrderController(IMediator mediator) : CreateOrderApiController
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    public override async Task<IActionResult> CreateOrder([FromBody] NewOrder newOrder)
    {
        try
        {
            var response = await _mediator.Send(
                new CreateOrderCommand(newOrder.Id, newOrder.Address.Country, newOrder.Address.City, newOrder.Address.Street, newOrder.Address.House, newOrder.Address.Apartment,
                    newOrder.Volume));
            if (response.Ok)
                return Created(
                    $"/api/orders/{response.Id}", 
                    new CreateCourierResponse() { CourierId = response.Id });
        
            return StatusCode(StatusCodes.Status409Conflict, new Error(StatusCodes.Status409Conflict, "Ошибка при создании курьера."));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Error(StatusCodes.Status500InternalServerError, ex.Message));
        }
    }
}
