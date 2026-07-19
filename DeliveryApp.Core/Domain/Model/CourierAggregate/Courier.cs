#nullable disable

using Ddd;
using DeliveryApp.Core.Domain.Model.SharedKernel;
using DeliveryApp.Core.Domain.Model.AssignmentAggregate;

namespace DeliveryApp.Core.Domain.Model.CounterAggegate;

public class Courier : Aggregate<Guid>
{
    #region Свойства

    private List<Assignment> _assignments = new List<Assignment>();
    private Location _location;

    /// <summary>
    /// Список заданий курьера
    /// </summary>
    public IReadOnlyCollection<Assignment> Assignments => _assignments.AsReadOnly();

    /// <summary>
    /// Локация курьера
    /// </summary>
    public Location Location 
    { 
        get => _location;
        set
        {
            if (value == null) throw new ArgumentNullException(nameof(value)    , @"Локация курьера не может быть пустой");
            // Проверка на допустимое расстояние между текущей локацией и новой локацией
            if (_location != null && _location.Distance(value) > 1)
            {
                throw new CourierInvalideLocationException(_location, value);
            }

            _location = value;
        }
    }

    /// <summary>
    /// Максимальный объем заданий курьера
    /// </summary>
    public Volume MaxVolume { get; init; } = new Volume(20);

    /// <summary>
    /// Имя курьера
    /// </summary>
    public string Name { get; init; } = string.Empty;
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

    public void AddAssignment(Assignment assignment)
    {
        if (assignment is null) throw new ArgumentNullException(nameof(assignment), "Задание не может быть пустым");
        if (!CanAddAssignment(assignment.Volume)) throw new CourierMaxVolumeExceededException();
        assignment.CreateId(); 
        _assignments.Add(assignment);
    }

    public void MoveTo(Location newlocation)
    {
        if (newlocation is null) throw new ArgumentNullException(nameof(newlocation), "Локация не может быть пустой");
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
    public void CompliteAssigment (Assignment assignment, Location location)
    {
        if (!_assignments.Contains(assignment)) throw new ArgumentException("Задание не найдено", nameof(assignment));
        assignment.Complete(location);
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
