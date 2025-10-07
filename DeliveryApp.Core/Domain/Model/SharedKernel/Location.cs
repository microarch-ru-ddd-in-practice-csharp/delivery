using System.Diagnostics.CodeAnalysis;
using CSharpFunctionalExtensions;
using Primitives;

namespace DeliveryApp.Core.Domain.Model.SharedKernel;

/// <summary>
///     Координата
/// </summary>
public class Location : ValueObject
{
    private Location()
    {
    }

    /// <summary>
    /// Координата
    /// </summary>
    /// <param name="x">Горизонталь</param>
    /// <param name="y">Вертикаль</param>
    private Location(int x, int y) : this()
    {
        X = x;
        Y = y;
    }

    /// <summary>
    /// Горизонтальная координата
    /// </summary>
    public int X { get; }

    /// <summary>
    /// Вертикальная координата
    /// </summary>
    public int Y { get; }


    /// <summary>
    /// Минимальная координата
    /// </summary>
    public static Location MinLocation => new(1, 1);

    /// <summary>
    /// Максимальная координата
    /// </summary>
    public static Location MaxLocation => new(10, 10);

    /// <summary>
    /// Создать координату
    /// </summary>
    /// <param name="x">Горизонталь</param>
    /// <param name="y">Вертикаль</param>
    /// <returns>Результат</returns>
    public static Result<Location, Error> Create(int x, int y)
    {
        if (x < MinLocation.X || x > MaxLocation.X) return GeneralErrors.ValueIsInvalid(nameof(x));
        if (y < MinLocation.Y || y > MaxLocation.Y) return GeneralErrors.ValueIsInvalid(nameof(y));

        return new Location(x, y);
    }

    /// <summary>
    /// Рассчитать расстояние между двумя Location
    /// </summary>
    /// <param name="destination">Конечная координата</param>
    /// <returns>Результат</returns>
    public Result<int, Error> DistanceTo(Location destination)
    {
        if (destination == null) return GeneralErrors.ValueIsRequired(nameof(destination));

        // Считаем разницу
        var diffX = Math.Abs(X - destination.X);
        var diffY = Math.Abs(Y - destination.Y);

        // Считаем дистанцию
        var distance = diffX + diffY;
        return distance;
    }

    /// <summary>
    /// Создать рандомную координату
    /// </summary>
    /// <returns>Результат</returns>
    public static Location CreateRandom()
    {
        var random = new Random(Guid.NewGuid().GetHashCode());
        var x = random.Next(MinLocation.X, MaxLocation.X + 1);
        var y = random.Next(MinLocation.Y, MaxLocation.Y + 1);
        var location = new Location(x, y);
        return location;
    }

    /// <summary>
    /// Перегрузка для определения идентичности
    /// </summary>    
    [ExcludeFromCodeCoverage]
    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return X;
        yield return Y;
    }
}