using DeliveryApp.Core.Ports;
using MediatR;

namespace DeliveryApp.Core.Application.UseCases.Queries.GetAllCouriers;

public class GetAllCouriersQueryHandler : IRequestHandler<GetAllCouriersQuery, IEnumerable<GetAllCouriersQueryDto>>
{
    private readonly ICourierRepository _courierRepository;
    public GetAllCouriersQueryHandler(ICourierRepository courierRepository)
    {
        _courierRepository = courierRepository ?? throw new ArgumentNullException(nameof(courierRepository));
    }

    public async Task<IEnumerable<GetAllCouriersQueryDto>> Handle(GetAllCouriersQuery request, CancellationToken cancellationToken)
    {
        return await _courierRepository.GetAllCouriersDtosAsync(cancellationToken);
    }
}
