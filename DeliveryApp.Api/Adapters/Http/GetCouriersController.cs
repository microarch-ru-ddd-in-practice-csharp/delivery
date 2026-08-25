using AutoMapper;
using DeliveryApp.Api.Adapters.Http.Contract.OpenApi.Controllers;
using DeliveryApp.Api.Adapters.Http.Contract.OpenApi.Models;
using DeliveryApp.Core.Ports;
using Microsoft.AspNetCore.Mvc;
namespace DeliveryApp.Api.Adapters.Http;

public class GetCouriersController(ICourierRepository courierRepository, IMapper mapper) : GetCouriersApiController
{
    private readonly ICourierRepository _courierRepository = courierRepository ?? throw new ArgumentNullException(nameof(courierRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public override async Task<IActionResult> GetCouriers()
    {
        try
        {
            var response = await _courierRepository.GetAllAsync(default);
            var model = _mapper.Map<List<Courier>>(response);
            return Ok(model);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Error(StatusCodes.Status500InternalServerError, ex.Message));
        }
    }
}
