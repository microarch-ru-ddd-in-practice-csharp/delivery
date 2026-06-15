using CSharpFunctionalExtensions;
using Errs;

namespace DeliveryApp.Core.Domain.Models
{
    public class Location : ValueObject
    {
        private const int MinCoordinate = 1;
        private const int MaxCoordinate = 10;
        
        public int X { get; }
        public int Y { get; }

        private Location(){}

        private Location(int x, int y) : this()
        {
            X = x;
            Y = y;
        }

        public static Result<Location, Error> Create(int x, int y)
        {
            if (x is < MinCoordinate or > MaxCoordinate)
                return GeneralErrors.ValueMustBeBetween<int>("x",  x, MinCoordinate, MaxCoordinate);
            if (y is < MinCoordinate or > MaxCoordinate)
                return GeneralErrors.ValueMustBeBetween<int>("y", y, MinCoordinate, MaxCoordinate);
            return new Location(x, y);
        }

        public Result<int, Error> DistanceTo(Location target)
        {
            if (target == null)
                return GeneralErrors.ValueIsRequired(nameof(target));
            var distance = Math.Abs(target.X - X) + Math.Abs(target.Y - Y);
            return distance;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return X;
            yield return Y;
        }
    }
}
