using DeliveryApp.Core;
using DeliveryApp.Core.Domain.Model.SharedKernel;
using DeliveryApp.Core.Ports;
using Grpc.Net.Client;
using Grpc.Net.Client.Configuration;
using Microsoft.Extensions.Options;

namespace DeliveryApp.Infrastructure.Adapters.Gprc.GeoService;

public class GeoClient : IGeoClient
{
    private readonly MethodConfig _methodConfig;
    private readonly SocketsHttpHandler _socketsHttpHandler;
    private readonly string _url;

    public GeoClient(IOptions<Settings> options)
    {
        if (string.IsNullOrWhiteSpace(options.Value.GeoServiceGrpcHost))
            throw new ArgumentException(nameof(options.Value.GeoServiceGrpcHost));
        _url = options.Value.GeoServiceGrpcHost;

        _socketsHttpHandler = new SocketsHttpHandler
        {
            PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
            KeepAlivePingDelay = TimeSpan.FromSeconds(60),
            KeepAlivePingTimeout = TimeSpan.FromSeconds(30),
            EnableMultipleHttp2Connections = true
        };
        _methodConfig = new MethodConfig
        {
            Names = { MethodName.Default },
            RetryPolicy = new RetryPolicy
            {
                MaxAttempts = 5,
                InitialBackoff = TimeSpan.FromSeconds(1),
                MaxBackoff = TimeSpan.FromSeconds(5),
                BackoffMultiplier = 2,
                RetryableStatusCodes = { Grpc.Core.StatusCode.Unavailable }
            }
        };
    }

    public async Task<Location> GetLocationAsync(string street, CancellationToken cancellationToken)
    {
        using var channel = GrpcChannel.ForAddress(_url, new GrpcChannelOptions
        {
            HttpHandler = _socketsHttpHandler,
            ServiceConfig = new ServiceConfig { MethodConfigs = { _methodConfig } }
        });

        var client = new Clients.Geo.Geo.GeoClient(channel);
        var response = await client.GetGeolocationAsync(new Clients.Geo.GetGeolocationRequest { Street = street }, cancellationToken: cancellationToken);
        return new Location(response.Location.X, response.Location.Y);
    }
}
