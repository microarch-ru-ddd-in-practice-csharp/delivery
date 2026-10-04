using CSharpFunctionalExtensions;

namespace DeliveryApp.Core.Domain.Model.SharedKernel;

public class Volume : ValueObject
{
    #region Свойства

    public int Capatity { get; private set; } = 1;

    #endregion

    #region Constructor

    private Volume ()
    { }

    public Volume(int capatity) : this()
    {
        if (capatity <= 0) throw new ArgumentException("Обем должен быть не меньше 1", nameof(capatity));
        this.Capatity = capatity;
    }

    #endregion

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Capatity;
    }
}
