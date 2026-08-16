#nullable disable
using CSharpFunctionalExtensions;

namespace DeliveryApp.Core.Domain.Model.CourierAggregate;

public class AssignmentStatus : Entity<int>
{
    public static AssignmentStatus Assigned => new(1, nameof(Assigned).ToLowerInvariant());
    public static AssignmentStatus Completed => new(2, nameof(Completed).ToLowerInvariant());

    private AssignmentStatus()
    { }

    private AssignmentStatus(int id, string name) : this()
    {
        this.Id= id;
        this.Name = name;
    }

    /// <summary>
    ///     Название статуса заказа
    /// </summary>
    public string Name { get; private set; }

    public static IEnumerable<AssignmentStatus> List() => new[] { Assigned, Completed };

}
