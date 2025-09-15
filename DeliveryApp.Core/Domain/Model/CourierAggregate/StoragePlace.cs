using System.Diagnostics.CodeAnalysis;
using CSharpFunctionalExtensions;
using Primitives;

public class StoragePlace : Entity<Guid>
{
    [ExcludeFromCodeCoverage]
    private StoragePlace()
    { }

    private StoragePlace(string name, int volume) : this()
    {
        Id = Guid.NewGuid();
        Name = name;
        TotalVolume = volume;
    }

    /// <summary>
    ///  Наименование
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// Всего
    /// </summary>
    public int TotalVolume { get; private set; }

    /// <summary>
    /// Идентификатор заказа
    /// </summary>    
    public Guid? OrderId { get; private set; }

    /// <summary>
    /// Создать место
    /// </summary>
    /// <param name="name">Название</param>
    /// <param name="volume">Всего</param>
    /// <returns>Результат</returns>
    public static Result<StoragePlace, Error> Create(string name, int volume)
    {
        if (string.IsNullOrEmpty(name)) return GeneralErrors.ValueIsRequired(nameof(name));
        if (volume <= 0) return GeneralErrors.ValueIsRequired(nameof(volume));

        return new StoragePlace(name, volume);
    }

    /// <summary>
    /// Проверка на занятость
    /// </summary>
    /// <returns>Да/Нет</returns>
    private bool IsOccupied()
    {
        return OrderId!=null;
    }    

    /// <summary>
    /// Можно ли занять место?
    /// </summary>
    /// <param name="volume">Объем</param>
    /// <returns>Результат</returns>
    public Result<bool, Error> CanStore(int volume)
    {
        if (volume <= 0) return GeneralErrors.ValueIsRequired(nameof(volume));
        if (IsOccupied()) return false;

        return volume <= TotalVolume;
    }    
    
    /// <summary>
    /// Занять место
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <param name="volume">Объем</param>
    /// <returns>Результат</returns>
    public UnitResult<Error> Store(Guid orderId, int volume)
    {
        if (orderId == Guid.Empty) return GeneralErrors.ValueIsRequired(nameof(orderId));
        if (volume <= 0) return GeneralErrors.ValueIsRequired(nameof(volume));

        var canStoreResult = CanStore(volume);
        if (canStoreResult.IsFailure) return canStoreResult.Error;
        var canStore = canStoreResult.Value;
        if (!canStore)
        {
            return Errors.ErrorCannotStoreOrderInThisStoragePlace();
        }

        OrderId = orderId;
        return UnitResult.Success<Error>();
    }
    
    /// <summary>
    /// Освободить место хранения
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <returns>Результат</returns>
    public UnitResult<Error> Clear(Guid orderId)
    {
        if (orderId == Guid.Empty) return GeneralErrors.ValueIsRequired(nameof(orderId));
        if (OrderId!=orderId) return Errors.ErrorOrderNotStoredInThisPlace();
        
        OrderId = null;
        return UnitResult.Success<Error>();
    }   
    
    
    /// <summary>
    /// Ошибки
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class Errors
    {
        public static Error ErrorCannotStoreOrderInThisStoragePlace()
        {
            return new Error($"{nameof(StoragePlace).ToLowerInvariant()}.cannot.store.order.in.this.storage.place",
                "Нельзя поместить заказ в это место хранения");
        }
        
        public static Error ErrorOrderNotStoredInThisPlace()
        {
            return new Error($"{nameof(StoragePlace).ToLowerInvariant()}.order.is.not.stored.in.this.place",
                "В месте хранения нет заказа, который пытаются извлечь");
        }
    }
}