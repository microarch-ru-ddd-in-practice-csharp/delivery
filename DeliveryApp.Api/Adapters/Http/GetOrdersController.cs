using AutoMapper;
using DeliveryApp.Api.Adapters.Http.Contract.OpenApi.Controllers;
using DeliveryApp.Api.Adapters.Http.Contract.OpenApi.Models;
using DeliveryApp.Core.Ports;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Api.Adapters.Http;

public class GetOrdersController (IOrderRepository orderRepository, IMapper mapper)  : GetOrdersApiController
{
    private readonly IOrderRepository _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public override async Task<IActionResult> GetOrders()
    {
        try
        {
            var response = await _orderRepository.GetNotCompletedOrdersAsync(default);
            var model = _mapper.Map<List<DeliveryApp.Api.Adapters.Http.Contract.OpenApi.Models.Order>>(response);
            return Ok(model);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Error(StatusCodes.Status500InternalServerError, ex.Message));
        }
    }
}
