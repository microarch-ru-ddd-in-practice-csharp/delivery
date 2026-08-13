using MediatR;

namespace DeliveryApp.Core.Application.UseCases.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<bool>
{

    public CreateOrderCommand(Guid orderId, string country, string city, string street, string house, string apartment, int volume)
    {
        OrderId = orderId;
        Country = country ?? throw new ArgumentNullException(nameof(country));
        City = city ?? throw new ArgumentNullException(nameof(city));
        Street = street ?? throw new ArgumentNullException(nameof(street));
        House = house ?? throw new ArgumentNullException(nameof(house));
        Apartment = apartment ?? throw new ArgumentNullException(nameof(apartment));
        Volume = volume;
    }

    public Guid OrderId { get; }

    public string Country { get; }

    public string City { get; }

    public string Street { get; }

    public string House { get; }

    public string Apartment { get; }

    public int Volume { get; }
}


