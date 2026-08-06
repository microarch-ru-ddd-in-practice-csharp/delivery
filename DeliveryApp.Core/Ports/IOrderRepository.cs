using DeliveryApp.Core.Domain.Model.OrderAggegate;

namespace DeliveryApp.Core.Ports;

/// <summary>
/// Интерфейс репозитория заказов
/// </summary>
public interface IOrderRepository
{
    /// <summary>
    /// Добавить заказ
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    public Task AddAsync(Order order);
    /// <summary>
    /// Обновить заказ
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    public Task UpdateAsync(Order order);
    /// <summary>
    /// Получить заказ по идентификатору
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public Task<Order> GetByIdAsync(Guid orderId, CancellationToken cancellationToken);
    /// <summary>
    /// Получить любой новый заказ
    /// </summary>
    /// <returns></returns>
    public Task<Order> GetAnyCreatedOrderAsync(CancellationToken cancellationToken);
    /// <summary>
    /// Получить все назначенные заказы
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<Order>> GetAssignedOrdersAsync(CancellationToken cancellationToken);

}
