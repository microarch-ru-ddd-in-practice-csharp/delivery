#nullable disable

using Ddd;
using DeliveryApp.Core.Domain.Model.AssignmentAggregate;
using DeliveryApp.Core.Domain.Model.OrderAggegate;
using DeliveryApp.Core.Domain.Model.SharedKernel;

namespace DeliveryApp.Core.Domain.Model.CounterAggegate;

public class Courier : Aggregate<Guid>
{
    #region Свойства

    private List<Assignment> _assignments = new List<Assignment>();

    /// <summary>
    /// Список заданий курьера
    /// </summary>
    public List<Assignment> Assignments => _assignments;

    /// <summary>
    /// Локация курьера
    /// </summary>
    public Location Location { get; private set; }
    
    /// <summary>
    /// Максимальный объем заданий курьера
    /// </summary>
    public Volume MaxVolume { get; private set; } = new Volume(20);

    /// <summary>
    /// Имя курьера
    /// </summary>
    public string Name { get; private set; } = string.Empty;
    /// <summary>
    /// Истина если курьер может принять еще одно задание (открытых по обьёму), иначе ложь
    /// </summary>
    public bool CanAddAssignment (Volume volume)
    {
        var currentVolume = _assignments.Where(a => a.Status != AssignmentStatus.Completed).Sum(a => a.Volume.Capatity);
        return currentVolume + volume.Capatity <= MaxVolume.Capatity;
    }

    #endregion

    #region Function

    /// <summary>
    /// Проверяет имеется ли Assignment с OrderId
    /// </summary>
    /// <param name="orderId"></param>
    /// <returns></returns>
    public bool ContainsOrderId (Guid orderId)
    {
        return Assignments.Any(x => x.OrderId == orderId);
    }

    /// <summary>
    /// Добавлет новый заказ, все параметры
    /// </summary>
    /// <param name="orderId"></param>
    /// <param name="volume"></param>
    /// <param name="location"></param>
    /// <exception cref="CourierMaxVolumeExceededException"></exception>
    public void AddAssignment(Guid orderId, Volume volume, Location location)
    {
        var newAssignment = new Assignment(orderId, volume, location, AssignmentStatus.Assigned);
        if (!CanAddAssignment(newAssignment.Volume)) throw new CourierMaxVolumeExceededException();
        newAssignment.CreateId();
        _assignments.Add(newAssignment);
        newAssignment.SetCourierId (Id);
    }

    /// <summary>
    /// Добавлет новый заказ, параметер сам заказ
    /// </summary>
    /// <param name="order"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public void AddAssignment(Order order)
    {
        if (order is null) throw new ArgumentNullException(nameof(order), "Заказ не может быть пустой");
        AddAssignment(order.Id, order.Volume, order.Location);
    }

    /// <summary>
    /// Заменяет локацию Курьера на новую.
    /// </summary>
    /// <param name="newlocation"></param>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="CourierInvalideLocationException"></exception>
    public void Move(Location newlocation)
    {
        if (newlocation is null) throw new ArgumentNullException(nameof(newlocation), "Локация не может быть пустой");
        if (Location.Distance(newlocation) > 1)
        {
            throw new CourierInvalideLocationException(Location, newlocation);
        }

        this.Location = newlocation;
    }

    #endregion

    #region Constructor

    public Courier(string name, Location location) : this()
    {
        this.Id = Guid.NewGuid();
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Имя курьера не может быть пустым", nameof(name));
        this.Name = name;
        this.Location = location ?? throw new ArgumentNullException(nameof(location), "Место доставки заказа не может быть пустым");
    }

    private Courier()
    { }

    /// <summary>
    /// Завершает заказ с локацией Курьера
    /// </summary>
    /// <param name="assignment"></param>
    /// <exception cref="ArgumentException"></exception>
    public void CompliteAssigment (Guid orderid)
    {
        var assignment = Assignments.FirstOrDefault(x => x.OrderId == orderid);
        if (assignment == null) throw new ArgumentException("Задание не найдено");
        assignment.Complete(this.Location);
    }

    public override string ToString()
    {
        return $"{Id} {Name}";
    }

    #endregion

    #region Exception

    public class CourierInvalideLocationException : Exception
    {
        public CourierInvalideLocationException(Location currentLocation, Location newLocation) : base("Новая локация слишком далеко от текущей")
        {
            CurrentLocation = currentLocation;
            NewLocation = newLocation;
        }

        public Location CurrentLocation { get; }
        public int Distance => CurrentLocation.Distance(NewLocation);
        public Location NewLocation { get; }
    }

    public class CourierMaxVolumeExceededException : Exception
    {
        public CourierMaxVolumeExceededException() : base("Курьер не может принять задание, так как превышен максимальный объем заданий")
        {
        }
    }
    #endregion
}
