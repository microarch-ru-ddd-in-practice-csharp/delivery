#nullable disable

using CSharpFunctionalExtensions;

namespace DeliveryApp.Core.Domain.Model.OrderAggegate
{
    public class OrderStatus : Entity<int>
    {
        public static OrderStatus Created => new(1, nameof(Created).ToLowerInvariant());

        public static OrderStatus Assigned => new(2, nameof(Assigned).ToLowerInvariant());
        public static OrderStatus Completed => new(3, nameof(Completed).ToLowerInvariant());

        private OrderStatus()
        { }

        private OrderStatus(int id, string name) : this()
        {
            this.Id = id;
            this.Name = name;
        }

        /// <summary>
        ///     Название статуса заказа
        /// </summary>
        public string Name { get; private set; } = "";

        public override string ToString()
        {
            return Name;
        }
        
        public static IEnumerable<OrderStatus> List() => [Created, Assigned, Completed];
    }
}
