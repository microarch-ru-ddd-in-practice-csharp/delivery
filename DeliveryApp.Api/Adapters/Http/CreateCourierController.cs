using AutoMapper;
using DeliveryApp.Api.Adapters.Http.Contract.OpenApi.Controllers;
using DeliveryApp.Api.Adapters.Http.Contract.OpenApi.Models;
using DeliveryApp.Core.Application.UseCases.Commands.CreateCourier;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Api.Adapters.Http;

public class CreateCourierController (IMediator mediator, IMapper mapper) : CreateCourierApiController
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    public override async Task<IActionResult> CreateCourier([FromBody] NewCourier newCourier)
    {
        if (string.IsNullOrEmpty(newCourier.Name))
        {
            return BadRequest(new Error("Имя курьера не может быть пустым."));
        }

        try
        {
            var responce = await _mediator.Send(new CreateCourierCommand(newCourier.Name));
            if (!responce.Ok)
            {
                return StatusCode(StatusCodes.Status409Conflict, new Error(StatusCodes.Status409Conflict, "Ошибка при создании курьера."));
            }
            return Created(
                    $"/api/couriers",
                    new CreateCourierResponse() { CourierId = responce.Id });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Error(StatusCodes.Status500InternalServerError, ex.Message));
        }
    }
}
