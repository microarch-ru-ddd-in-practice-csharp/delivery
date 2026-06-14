using CSharpFunctionalExtensions;
using Errs;

namespace DeliveryApp.Core.Domain.Models
{
    public class Location : ValueObject
    {
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
            if (x is < 1 or > 10 )
                return GeneralErrors.ValueMustBeBetween<int>("x",  x, 1, 10 );
            if (y is < 1 or > 10 )
                return GeneralErrors.ValueMustBeBetween<int>("y", y, 1, 10 );
            return new Location(x, y);
        }

        public static Location CreateRandom()
        {
            var randX = Random.Shared.Next(1, 11);
            var randY = Random.Shared.Next(1, 11);
            var randomLoc = new Location(randX, randY);
            return randomLoc;
        }
        
        public Result<int, Error> DistanceTo(Location target)
        {
            if (target == null)
                return GeneralErrors.ValueIsRequired(nameof(target));
            var myX = X;
            var myY = Y;
            var distance = Math.Abs(target.X - myX) + Math.Abs(target.Y - myY);
            return distance;
        }
        
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return X;
            yield return Y;
        }
    }
}
