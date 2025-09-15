using System.Diagnostics.CodeAnalysis;
using CSharpFunctionalExtensions;

namespace DeliveryApp.Core.Domain.Model.OrderAggregate;

/// <summary>
/// Статус заказа
/// </summary>
public class OrderStatus : ValueObject
{
    /// <summary>
    /// Статус заказа
    /// </summary>
    [ExcludeFromCodeCoverage]
    private OrderStatus()
    {
    }

    /// <summary>
    /// Статус заказа 
    /// </summary>
    /// <param name="name">Название</param>
    private OrderStatus(string name) : this()
    {
        Name = name;
    }

    public static OrderStatus Created => new(nameof(Created).ToLowerInvariant());
    public static OrderStatus Assigned => new(nameof(Assigned).ToLowerInvariant());
    public static OrderStatus Completed => new(nameof(Completed).ToLowerInvariant());

    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }
}
