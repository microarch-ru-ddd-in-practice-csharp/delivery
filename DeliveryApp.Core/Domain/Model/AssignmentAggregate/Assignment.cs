#nullable disable

using CSharpFunctionalExtensions;
using DeliveryApp.Core.Domain.Model.CounterAggegate;
using DeliveryApp.Core.Domain.Model.SharedKernel;
using Errs;

namespace DeliveryApp.Core.Domain.Model.AssignmentAggregate;

public class Assignment : Entity<Guid>
{
    #region Свойства

    /// <summary>
    /// идентификатор заказа
    /// </summary>
    public Guid OrderId { get; init; }

    /// <summary>
    /// Обьем заказа
    /// </summary>
    public Volume Volume { get; init; }

    /// <summary>
    /// Место доставки заказа
    /// </summary>
    public Location Location { get; init; }

    /// <summary>
    /// Статус заказа
    /// </summary>
    public AssignmentStatus Status { get; private set; }


    #endregion

    #region functions

    /// <summary>
    /// 
    /// </summary>
    /// <param name="location"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public void Complete(Location location)
    {
        if (Status == AssignmentStatus.Completed)  throw new AssignmentAlreadyCompletedException();
        if (location == null) throw new ArgumentNullException("Локация");
        if (!location.IsSameLocation(Location)) throw new AssignmentCourierNotSameLocationException();

        this.Status = AssignmentStatus.Completed;
    }

    #endregion

    #region Constructor

    private Assignment()
    { }

    public Assignment(Guid orderId, Volume volume, Location location, AssignmentStatus status) : this()
    {
        if (orderId == Guid.Empty) throw new ArgumentException("Идентификатор заказа не может быть пустым", nameof(orderId));
        if (status == AssignmentStatus.Completed) throw new ArgumentException("Статус не може быть ,завершенным,");
        this.Id = Guid.NewGuid();
        this.OrderId = orderId;
        this.Volume = volume ?? throw new ArgumentNullException(nameof(volume), "Обьем заказа не может быть пустым");
        this.Location = location ?? throw new ArgumentNullException(nameof(location), "Место доставки заказа не может быть пустым");
        this.Status = status ?? throw new ArgumentNullException(nameof(status), "Статус заказа не может быть пустым");
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
