using CSharpFunctionalExtensions;
using System.Diagnostics.CodeAnalysis;

namespace DeliveryApp.Core.Domain.Model.OrderAggregate
{
    public class OrderStatus : ValueObject
    {
        public static readonly OrderStatus Created = new(nameof(Created).ToLowerInvariant());
        public static readonly OrderStatus Assign = new(nameof(Assign).ToLowerInvariant());
        public static readonly OrderStatus Completed = new(nameof(Completed).ToLowerInvariant());

        /// <summary>
        /// Дефолтный конструктов для ORM
        /// </summary>
        [ExcludeFromCodeCoverage]
        private OrderStatus()
        {
        }

        private OrderStatus(string name)
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
