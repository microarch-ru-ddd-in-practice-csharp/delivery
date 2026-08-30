using DeliveryApp.Core.Domain.Model.SharedKernel;
using MediatR;

namespace DeliveryApp.Core.Application.UseCases.Commands.CreateOrderCommand;

public class CreateOrderCommand : IRequest<CreateResponse>
{

    public CreateOrderCommand(Guid orderId, string country, string city, string street, string house, string apartment, int volume)
    {
        if (orderId == Guid.Empty) throw new ArgumentException("ID Заказа не может быть пустым");
        OrderId = orderId;
        Country = country ?? throw new ArgumentNullException(nameof(country));
        City = city ?? throw new ArgumentNullException(nameof(city));
        Street = street ?? throw new ArgumentNullException(nameof(street));
        House = house ?? throw new ArgumentNullException(nameof(house));
        Apartment = apartment ?? throw new ArgumentNullException(nameof(apartment));
        Volume = new Volume(volume);
    }

    public Guid OrderId { get; }

    public string Country { get; }

    public string City { get; }

    public string Street { get; }

    public string House { get; }

    public string Apartment { get; }

    public Volume Volume { get; }
}


