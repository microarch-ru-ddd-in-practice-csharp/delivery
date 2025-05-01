using System.Diagnostics.CodeAnalysis;
using CSharpFunctionalExtensions;
using DeliveryApp.Core.Domain.Model.SharedKernel;
using Primitives;

namespace DeliveryApp.Core.Domain.Model.CourierAggregate
{
    public class Transport : Entity<Guid>
    {
        /// <summary>
        /// Дефолтный конструктов для ORM
        /// </summary>
        [ExcludeFromCodeCoverage]
        private Transport()
        {
        }

        /// <summary>
        /// Конструктор для создания транспорта
        /// </summary>
        /// <param name="name">Название транспорта</param>
        /// <param name="speed">Скорость транспорта</param>
        private Transport(string name, Speed speed)
        {
            Id = Guid.NewGuid();
            Name = name;
            Speed = speed;
        }

        public string Name { get; private set; }

        public Speed Speed { get; private set; }

        public static Result<Transport, Error> Create(string name, Speed speed)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Errors.TransportNameIsRequired();
            }

            if (speed == null)
            {
                return Errors.SpeedIsRequired();
            }

            return new Transport(name, speed);
        }

        public Result<Transport, Error> SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return Errors.TransportNameIsRequired();
            }

            Name = name;
            return this;
        }

        public Result<Transport, Error> SetSpeed(Speed speed)
        {
            if (speed == null)
            {
                return Errors.SpeedIsRequired();
            }

            Speed = speed;
            return this;
        }

        public Location Move(Location from, Location to)
        {
            var distance = from.DistanceTo(to);
            var step = Speed.Value;

            // Если расстояние до точки назначения меньше или равно скорости, то можно сразу перейти в точку назначения
            if (distance == 0 || distance <= step)
            {
                return Location.Create(to.X, to.Y).Value;
            }

            var dx = to.X - from.X;

            //смещение по X
            var deltaX = Math.Sign(dx) * Math.Min(Math.Abs(dx), step);
            var rem = step - Math.Abs(deltaX);

            //смещение по Y c учетом оставшегося расстояния
            var dy = to.Y - from.Y;
            var deltaY = Math.Sign(dy) * Math.Min(Math.Abs(dy), rem);

            // финальная точка
            var newX = from.X + deltaX;
            var newY = from.Y + deltaY;

            return Location.Create(newX, newY).Value;
        }


        public static class Errors
        {
            public static Error TransportNameIsRequired()
            {
                return new Error($"{nameof(Transport).ToLowerInvariant()}.name.is.required", "Transport name is required");
            }

            public static Error SpeedIsRequired()
            {
                return new Error($"{nameof(SharedKernel.Speed).ToLowerInvariant()}.is.required", "Speed is required");
            }
        }
    }
}
