using System.Diagnostics.CodeAnalysis;
using CSharpFunctionalExtensions;
using Primitives;

namespace DeliveryApp.Core.Domain.Model.SharedKernel
{
    public class Location : ValueObject
    {
        public const int Min = 1;
        public const int Max = 10;

        [ExcludeFromCodeCoverage]
        private Location()
        {
        }

        private Location(int x, int y)
            : this()
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Координата X
        /// </summary>
        public int X { get; }

        /// <summary>
        /// Координата Y
        /// </summary>
        public int Y { get; }

        /// <summary>
        /// Создание объекта Location
        /// </summary>
        /// <param name="x">Координата X</param>
        /// <param name="y">Координата Y</param>
        /// <returns>
        /// Объект Location
        /// </returns>
        public static Result<Location, Error> Create(int x, int y)
        {
            if (x is < Min or > Max)
            {
                return Errors.LocationValueXIsInvalid();
            }

            if (y is < Min or > Max)
            {
                return Errors.LocationValueYIsInvalid();
            }

            return new Location(x, y);
        }

        /// <summary>
        /// Создание объекта Location
        /// </summary>
        /// <returns>
        /// Объект Location
        /// </returns>
        public static Result<Location, Error> CreateRandom()
        {
            var random = new Random();
            var x = random.Next(Min, Max + 1);
            var y = random.Next(Min, Max + 1);
            return new Location(x, y);
        }

        public int DistanceTo(Location location)
        {
            var xDistance = Math.Abs(location.X - X);
            var yDistance = Math.Abs(location.Y - Y);
            return xDistance + yDistance;
        }

        /// <summary>
        /// Перегрузка идентичности
        /// </summary>
        /// <returns>
        /// Список свойств для сравнения с идентичности
        /// </returns>
        [ExcludeFromCodeCoverage]
        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return X;
            yield return Y;
        }

        public static class Errors
        {
            public static Error LocationValueXIsInvalid()
            {
                return new Error($"{nameof(Location).ToLowerInvariant()}.value.x.is.invalid", $"Location value X should be between {Min} and {Max} values");
            }

            public static Error LocationValueYIsInvalid()
            {
                return new Error($"{nameof(Location).ToLowerInvariant()}.value.y.is.invalid", $"Location value Y should be between {Min} and {Max} values");
            }
        }
    }
}
