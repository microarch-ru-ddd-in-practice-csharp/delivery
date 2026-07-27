#nullable disable

using DeliveryApp.Core.Domain.Model.CounterAggegate;
using DeliveryApp.Core.Domain.Model.OrderAggegate;

namespace DeliveryApp.Core.Domain.Services.OrderAssignment;

/// <summary>
/// Имплементация сервиса IOrderAssignmentService
/// </summary>

public class OrderAssignmentService : IOrderAssignmentService
{
    public Courier AssignOrderToCourier(Order order, List<Courier> availableCouriers)
    { 
        if (order == null) throw new ArgumentNullException(nameof(order));
        if (order.Status != OrderStatus.Created) throw new NoOrderStatusCreatedException(order);
        // создайм список доступных курьеров
        var canAssigmentCourierList = availableCouriers.Where(x => x.CanAddAssignment(order.Volume)).ToList();
        if (!canAssigmentCourierList.Any())
            throw new NoAvailibaleCourirsFromVolumeException(availableCouriers);
        // выбираем ближайщего курьера к заказу по дистанции к заказу.
        var courier = canAssigmentCourierList.OrderBy(x => x.Location.Distance(order.Location)).First();
        // Переводим заказ в согласованное состояние
        courier.AddAssignment(order);
        // возвращяем курьера
        return courier;
    }

    #region Exceptions

    public class NoOrderStatusCreatedException : Exception
    {
        public Order Order { get; private init; }

        internal NoOrderStatusCreatedException(Order order) : base ("Заказ должен иметь стату создан")
        {
            Order = order;
        }
    }

    public class NoAvailibaleCourirsFromVolumeException : Exception
    {
        public List<Courier> Couriers { get; private init; }

        public NoAvailibaleCourirsFromVolumeException(List<Courier> couriers) : base("Нет курьеров с допустимых объем заказов.")
        {
            this.Couriers = couriers;
        }
    }


    #endregion
}
