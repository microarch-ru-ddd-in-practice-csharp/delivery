using CSharpFunctionalExtensions;
using DeliveryApp.Core.Domain.Model.OrderAggregate;
using Primitives;

namespace DeliveryApp.Core.Ports;

/// <summary>
///     Repository для Aggregate OrderDTO
/// </summary>
public interface IOrderRepository : IRepository<Order>
{
    /// <summary>
    /// Добавить заказ
    /// </summary>
    /// <param name="order">Заказ</param>
    /// <returns>Заказ</returns>
    Task AddAsync(Order order);

    /// <summary>
    /// Обновить заказ
    /// </summary>
    /// <param name="order">Заказ</param>
    void Update(Order order);

    /// <summary>
    /// Получить заказ
    /// </summary>
    /// <param name="orderId">Идентификатор</param>
    /// <returns>Заказ</returns>
    Task<Maybe<Order>> GetAsync(Guid orderId);

    /// <summary>
    /// Получить один заказ
    /// </summary>
    /// <returns>Заказы</returns>
    Task<Maybe<Order>> GetFirstInCreatedStatusAsync();

    /// <summary>
    /// Получить все назначенные заказы
    /// </summary>
    /// <returns>Заказы</returns>
    IEnumerable<Order> GetAllInAssignedStatus();
}
