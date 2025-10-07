using CSharpFunctionalExtensions;
using DeliveryApp.Core.Domain.Model.CourierAggregate;
using Primitives;

namespace DeliveryApp.Core.Ports;

/// <summary>
///     Repository для Aggregate CourierDto
/// </summary>
public interface ICourierRepository : IRepository<Courier>
{
    /// <summary>
    /// Добавить курьера
    /// </summary>
    /// <param name="courier">Курьер</param>
    /// <returns>Курьер</returns>
    Task AddAsync(Courier courier);

    /// <summary>
    /// Обновить курьера
    /// </summary>
    /// <param name="courier">Курьер</param>
    void Update(Courier courier);

    /// <summary>
    /// Получить курьера
    /// </summary>
    /// <param name="courierId">Идентификатор</param>
    /// <returns>Курьер</returns>
    Task<Maybe<Courier>> GetAsync(Guid courierId);

    /// <summary>
    /// Получить всех свободных курьеров
    /// </summary>
    /// <returns>Курьеры</returns>
    IEnumerable<Courier> GetAllFree();
}
