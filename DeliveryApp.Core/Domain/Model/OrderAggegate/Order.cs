#nullable disable

using Ddd;
using DeliveryApp.Core.Domain.Model.SharedKernel;
using System.Diagnostics.CodeAnalysis;

namespace DeliveryApp.Core.Domain.Model.OrderAggegate;


/// <summary>
/// Заказ на доставку
/// </summary>
public class Order : Aggregate<Guid>
{
    #region Свойства

    public Location Location { get; init; }

    public OrderStatus Status { get; private set; } = OrderStatus.Created;
    public Volume Volume { get; init; }

    #endregion  

    #region Constructor

    [ExcludeFromCodeCoverage]
    public Order(Guid id, Location location, Volume volume)
    {
        Id = id;
        Location = location ?? throw new ArgumentNullException(nameof(location), "Место доставки заказа не может быть пустым");
        Volume = volume ?? throw new ArgumentNullException(nameof(volume), "Объем заказа не может быть пустым");
    }

    [ExcludeFromCodeCoverage]
    private Order()
    {
    }

    #endregion

    #region functions

    /// <summary>
    /// Сменяет статус заказа на Назначен.
    /// </summary>
    public void Assign()
    {
        if (Status != OrderStatus.Created)
        {
            throw new OrderInvalidStatusException("Статус заказа может быть изменен на 'Назначенный' только из 'Созданного'", OrderStatus.Assigned);
        }

        Status = OrderStatus.Assigned;
    }

    /// <summary>
    /// Сменяет статус заказа на Завершен.
    /// </summary>
    /// <exception cref="OrderInvalidStatusException"></exception>
    public void Complete()
    {
        if (Status != OrderStatus.Assigned)
        {
            throw new OrderInvalidStatusException("Статус заказа может быть изменен на 'Завершенный' только из 'Назначенный'", OrderStatus.Completed);
        }

        Status = OrderStatus.Completed;
    }

    #endregion

    public class OrderInvalidStatusException : Exception
    {
        public OrderInvalidStatusException(string message, OrderStatus invalideStatus) : base(message)
        {
            Invalidestatus = invalideStatus;
        }

        public OrderStatus Invalidestatus { get; }
    }
}
