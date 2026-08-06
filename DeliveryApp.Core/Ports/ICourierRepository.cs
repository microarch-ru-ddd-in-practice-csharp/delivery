using System;
using System.Collections.Generic;
using System.Text;
using DeliveryApp.Core.Domain.Model.CounterAggegate;

namespace DeliveryApp.Core.Ports;

/// <summary>
/// Интерфейс репозитория курьеров
/// </summary>
public interface ICourierRepository
{
    /// <summary>
    /// Добавить курьера
    /// </summary>
    /// <param name="courier"></param>
    /// <returns></returns>
    public Task AddAsync(Courier courier);
    /// <summary>
    /// Обновить курьера
    /// </summary>
    /// <param name="courier"></param>
    /// <returns></returns>
    public Task UpdateAsync(Courier courier);
    /// <summary>
    /// Получить курьера по идентификатору
    /// </summary>
    /// <param name="courierId"></param>
    /// <returns></returns>
    public Task<Courier> GetByIdAsync(Guid courierId, CancellationToken cancellationToken);
    /// <summary>
    /// Получить всех курьеров
    /// </summary>
    /// <returns></returns>
    public Task<IEnumerable<Courier>> GetAllAsync(CancellationToken cancellationToken);
}
