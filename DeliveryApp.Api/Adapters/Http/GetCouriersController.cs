using AutoMapper;
using DeliveryApp.Api.Adapters.Http.Contract.OpenApi.Controllers;
using DeliveryApp.Api.Adapters.Http.Contract.OpenApi.Models;
using DeliveryApp.Core.Application.UseCases.Queries.GetAllCouriers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace DeliveryApp.Api.Adapters.Http;

public class GetCouriersController(IMediator mediator, IMapper mapper) : GetCouriersApiController
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    public override async Task<IActionResult> GetCouriers()
    {
        try
        {
            var response = await _mediator.Send(new GetAllCouriersQuery());
            var model = _mapper.Map<List<Courier>>(response);
            return Ok(model);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Error(StatusCodes.Status500InternalServerError, ex.Message));
        }
    }
}
