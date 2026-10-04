using AutoMapper;
using DeliveryApp.Api.Adapters.Http.Contract.OpenApi.Controllers;
using DeliveryApp.Api.Adapters.Http.Contract.OpenApi.Models;
using DeliveryApp.Core.Application.UseCases.Queries.GetNotCompletedOrdersQuery;
using DeliveryApp.Core.Ports;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Api.Adapters.Http;

public class GetOrdersController (IMediator mediator, IMapper mapper)  : GetOrdersApiController
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public override async Task<IActionResult> GetOrders()
    {
        try
        {
            var response = await _mediator.Send(new GetNotCompletedOrdersQuery());
            var model = _mapper.Map<List<DeliveryApp.Api.Adapters.Http.Contract.OpenApi.Models.Order>>(response);
            return Ok(model);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Error(StatusCodes.Status500InternalServerError, ex.Message));
        }
    }
}
