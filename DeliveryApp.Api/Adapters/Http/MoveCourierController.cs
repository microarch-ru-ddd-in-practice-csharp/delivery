using DeliveryApp.Api.Adapters.Http.Contract.OpenApi.Controllers;
using DeliveryApp.Api.Adapters.Http.Contract.OpenApi.Models;
using DeliveryApp.Core.Application.UseCases.Commands.MoveCourierCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace DeliveryApp.Api.Adapters.Http;

public class MoveCourierController (IMediator mediator) : MoveCourierApiController
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    public override async Task<IActionResult> MoveCourier([FromRoute(Name = "courierId")][Required] Guid courierId, [FromBody] Location location)
    {
        try
        {
            await _mediator.Send (new MoveCourierCommand(courierId, new DeliveryApp.Core.Domain.Model.SharedKernel.Location(location.X, location.Y)));
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new Error(StatusCodes.Status400BadRequest, ex.Message));
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new Error(StatusCodes.Status409Conflict, ex.Message));
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Error(StatusCodes.Status500InternalServerError, ex.Message));
        }
    }
}
