#nullable disable

using DeliveryApp.Core.Domain.Model.CourierAggregate;
using DeliveryApp.Core.Domain.Model.OrderAggegate;

namespace DeliveryApp.Core.Domain.Services.OrderAssignment;

/// <summary>
/// Имплементация сервиса IOrderAssignmentService
/// </summary>

public class OrderAssignmentService : IOrderAssignmentService
{
    public (bool Successful, Courier Courier) AssignOrderToCourier(Order order, List<Courier> availableCouriers)
    { 
        if (order == null) throw new ArgumentNullException(nameof(order));
        if (order.Status != OrderStatus.Created) throw new NoOrderStatusCreatedException(order);
        if (availableCouriers == null || !availableCouriers.Any())
            throw new NoAvailibaleCourierException(availableCouriers);

        // создайм список доступных курьеров
        var canAssigmentCourierList = availableCouriers.Where(x => x.CanAddAssignment(order.Volume)).ToList();
        if (!canAssigmentCourierList.Any()) return (false, null);
        // выбираем ближайщего курьера к заказу по дистанции к заказу.
        var courier = canAssigmentCourierList.OrderBy(x => x.Location.Distance(order.Location)).First();
        // Присоединяем заказ курьеру
        courier.AddAssignment(order);
        // Переводим заказ в согласованное состояние
        order.Assign();
        // возвращяем курьера
        return (true, courier);
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

    public class NoAvailibaleCourierFromVolumeException : Exception
    {
        public List<Courier> Couriers { get; private init; }

        public NoAvailibaleCourierFromVolumeException(List<Courier> couriers) : base("Нет курьеров с допустимых объем заказов.")
        {
            this.Couriers = couriers;
        }
    }

    public class NoAvailibaleCourierException : Exception
    {
        public List<Courier> Couriers { get; private init; }

        public NoAvailibaleCourierException(List<Courier> couriers) : base("Список курьеров пустой")
        {
            this.Couriers = couriers;
        }
    }


    #endregion
}
