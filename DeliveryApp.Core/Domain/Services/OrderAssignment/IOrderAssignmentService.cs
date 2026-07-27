using DeliveryApp.Core.Domain.Model.CounterAggegate;
using DeliveryApp.Core.Domain.Model.OrderAggegate;

namespace DeliveryApp.Core.Domain.Services.OrderAssignment;

/// <summary>
/// интерфайс Сервисаб распределяющего заказы на доступных курьеров
/// </summary>
public interface IOrderAssignmentService
{
    Courier AssignOrderToCourier(Order order, List<Courier> availableCouriers);
}
