#nullable disable

using CSharpFunctionalExtensions;
using DeliveryApp.Core.Domain.Model.SharedKernel;

namespace DeliveryApp.Core.Domain.Model.CourierAggregate;

public class Assignment : Entity<Guid>
{
    #region Свойства

    /// <summary>
    /// идентификатор заказа
    /// </summary>
    public Guid OrderId { get; private set; }

    /// <summary>
    /// Обьем заказа
    /// </summary>
    public Volume Volume { get; private set; }

    /// <summary>
    /// Место доставки заказа
    /// </summary>
    public Location Location { get; private set; }

    /// <summary>
    /// Статус заказа
    /// </summary>
    public AssignmentStatus Status { get; private set; }

    public Guid CourierId { get; private set; }

    public Courier Courier { get; private set; } = null!;


    #endregion

    #region functions

    /// <summary>
    /// Закрывает доставку, если курьер находится в той же локации, что и заказ
    /// </summary>
    /// <param name="location"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public void Complete(Location location)
    {
        if (Status == AssignmentStatus.Completed)  throw new AssignmentAlreadyCompletedException();
        if (location == null) throw new ArgumentNullException(nameof(location), "Локация не может быть пустой");
        if (location.Distance(Location) > 1) throw new AssignmentCourierNotSameLocationException();
        this.Status = AssignmentStatus.Completed;
    }

    public void CreateId()
    {
        this.Id = Guid.NewGuid();
    }

    internal void SetCourierId (Guid courierId)
    {
        CourierId = courierId;
    }

    #endregion

    #region Constructor

    private Assignment()
    {
        Status = AssignmentStatus.Assigned;
    }

    public Assignment(Guid orderId, Volume volume, Location location) : this()
    {
        if (orderId == Guid.Empty) throw new ArgumentException("Идентификатор заказа не может быть пустым", nameof(orderId));
        this.Id = Guid.NewGuid();
        this.OrderId = orderId;
        this.Volume = volume ?? throw new ArgumentNullException(nameof(volume), "Обьем заказа не может быть пустым");
        this.Location = location ?? throw new ArgumentNullException(nameof(location), "Место доставки заказа не может быть пустым");
    }
 
    #endregion

    #region Классы исключений

    public class AssignmentAlreadyCompletedException : Exception
    {
        public  AssignmentAlreadyCompletedException() : base("Статус уже Завершен.")
        { }
    }

    public class AssignmentCourierNotSameLocationException : Exception
    {
        public AssignmentCourierNotSameLocationException() : base("Курьер находится слишком далеко, завершить невозможно")
        { }
    }

    #endregion
}
