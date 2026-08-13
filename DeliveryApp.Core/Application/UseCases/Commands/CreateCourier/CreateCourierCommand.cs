using MediatR;

namespace DeliveryApp.Core.Application.UseCases.Commands.CreateCourier;

public class CreateCourierCommand : IRequest<bool>
{
    public CreateCourierCommand (string name)
    { 
        Name = name ?? throw new ArgumentNullException(nameof(name));
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Ямя курьера не может быть пустым или содержать только пробельные символы.", nameof(name));
    }

    public string Name { get; }
}
