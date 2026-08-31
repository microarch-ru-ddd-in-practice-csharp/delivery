using DeliveryApp.Core.Domain.Model.SharedKernel;

namespace DeliveryApp.Core.Ports;

public interface IGeoClient
{
    Task<Location> GetLocationAsync(string street, CancellationToken cancellationToken);
}
