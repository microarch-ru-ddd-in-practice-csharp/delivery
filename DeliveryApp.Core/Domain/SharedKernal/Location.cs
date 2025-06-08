using System.Diagnostics.CodeAnalysis;
using CSharpFunctionalExtensions;
using Primitives;

namespace DeliveryApp.Core.Domain.SharedKernal;

public class Location : ValueObject
{
    public int X { get; private set; }
    public int Y { get; private set; }

    [ExcludeFromCodeCoverage]
    private Location()
    { }

    public Location(int x_value, int y_value)
    {
        if (x_value < 0 || x_value > 10) throw new ArgumentOutOfRangeException(nameof(x_value), "Value must be within 1..10");
        if (y_value < 0 || y_value > 10) throw new ArgumentOutOfRangeException(nameof(y_value), "Value must be within 1..10");

        X = x_value;
        Y = y_value;
    }

    public int DistanceTo(Location other)
    {
        if (other is null) throw new ArgumentNullException(nameof(other));

        int dx = X - other.X;
        int dy = Y - other.Y;
        return Math.Abs(dx) + Math.Abs(dy);
    }

    public static Location CreateRandom()
    {
        int x = Random.Shared.Next(1, 11);
        int y = Random.Shared.Next(1, 11);
        return new Location(x, y);
    }

    [ExcludeFromCodeCoverage]
    protected override IEnumerable<IComparable> GetEqualityComponents()
    {
        yield return X;
        yield return Y;
    }
}