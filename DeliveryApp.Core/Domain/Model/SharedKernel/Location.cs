using CSharpFunctionalExtensions;

namespace DeliveryApp.Core.Domain.Model.SharedKernel;

public class Location : ValueObject
{
    #region Property

    /// <summary>
    /// горизонтальная координата
    /// </summary>
    public int X { get; private set; }

    /// <summary>
    /// вертикальная координата
    /// </summary>
    public int Y { get; private set; }

    #endregion

    #region Constructor

    /// <summary>
    /// Конструктор создания объекта Location
    /// X, Y должен быть в диапозоне 1-10
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <exception cref="ArgumentException"></exception>
    public Location(int x, int y)
    {
        if (!IsValueValid(x)) throw new ArgumentException($"Значение X:{x} недопустимо (1-10)!");
        if (!IsValueValid(y)) throw new ArgumentException($"Значение Y:{y} недопустимо (1-10)!");
        this.X = x;
        this.Y = y;
    }

    /// <summary>
    /// пустой конструктор
    /// </summary>
    private Location ()
    { }
    /// <summary>
    /// Возвращает расстояние между текущей локацией и целевой локацией
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public int Distance (Location target)
    {
        var res = Math.Abs(this.X - target.X) + Math.Abs(this.Y - target.Y);
        return res;
    }

    #endregion

    #region Functionen

    /// <summary>
    /// Проверяет, что текущая локация совпадает с целевой локацией
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>

    public bool IsSameLocation(Location target)

    {
        return this.Distance(target) == 0;
    }

    

    /// <summary>
    /// Создает случайную локацию в пределах 10х10
    /// </summary>
    /// <returns></returns>
    public static Location CreateRandom()
    {
        var random = new Random();
        int x = random.Next(1, 11);
        int y = random.Next(1, 11);
        return new Location(x, y);
    }

    public override string ToString()
    {
        return base.ToString() + $" (X: {X}, Y: {Y})";
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return X;
        yield return Y;
    }

    /// <summary>
    /// Проверяет, что значение координаты находится в пределах 1-10
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    private static bool IsValueValid(int value) => (value >= 1 && value <= 10);
    #endregion
}
