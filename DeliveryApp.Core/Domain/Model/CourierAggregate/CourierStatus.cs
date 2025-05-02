﻿using CSharpFunctionalExtensions;
using System.Diagnostics.CodeAnalysis;

namespace DeliveryApp.Core.Domain.Model.CourierAggregate
{
    public class CourierStatus : ValueObject
    {
        public static readonly CourierStatus Free = new(nameof(Free).ToLowerInvariant());
        public static readonly CourierStatus Busy = new(nameof(Busy).ToLowerInvariant());

        /// <summary>
        /// Дефолтный конструктов для ORM
        /// </summary>
        [ExcludeFromCodeCoverage]
        private CourierStatus()
        {
        }

        private CourierStatus(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        protected override IEnumerable<IComparable> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}
