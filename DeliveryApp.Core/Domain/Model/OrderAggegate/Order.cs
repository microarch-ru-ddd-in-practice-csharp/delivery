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

    private OrderStatus _orderStatus = OrderStatus.Created;
    public Location Location { get; init; }

    public OrderStatus Status
    { 
        get => _orderStatus;
        set
        {
            if (value is null) throw new ArgumentNullException(nameof(value), "Статус заказа не может быть пустым");
            if (value == _orderStatus) return;
            if (value == OrderStatus.Assigned && _orderStatus != OrderStatus.Created)
            {
                throw new OrderInvalidStatusException("Статус заказа может быть изменен на 'Назначенный' только из 'Созданного'", value);  
            }
            else if (value == OrderStatus.Completed && _orderStatus != OrderStatus.Assigned)
            {
                throw new OrderInvalidStatusException("Статус заказа может быть изменен на 'Завершенный' только из 'Назначенный'", value);
            }
            
            _orderStatus = value;
        }
    }
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
    { }

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
