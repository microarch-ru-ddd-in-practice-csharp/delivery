﻿using CSharpFunctionalExtensions;
using Primitives;
using System.Diagnostics.CodeAnalysis;

namespace DeliveryApp.Core.Domain.Model.SharedKernel
{
    public class Speed : ValueObject
    {
        public const byte Min = 1;
        public const byte Max = 3;

        [ExcludeFromCodeCoverage]
        private Speed()
        {
        }

        /// <summary>
        /// Создание скорости
        /// </summary>
        /// <param name="value">Значение скорости</param>
        private Speed(byte value)
        {
            Value = value;
        }

        public byte Value { get; }

        public static Result<Speed, Error> Create(byte value)
        {
            if (value is < Min or > Max)
            {
                return Errors.SpeedValueIsInvalid();
            }
            return new Speed(value);
        }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Value;
        }

        public static class Errors
        {
            public static Error SpeedValueIsInvalid()
            {
                return new Error($"{nameof(Speed).ToLowerInvariant()}.value.is.invalid", $"Speed must be between {Min} and {Max}.");
            }
        }
    }
}
