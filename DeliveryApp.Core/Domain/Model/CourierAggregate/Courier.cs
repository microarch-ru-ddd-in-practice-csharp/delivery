#nullable disable

using CSharpFunctionalExtensions;
using DeliveryApp.Core.Domain.Model.AssignmentAggregate;
using DeliveryApp.Core.Domain.Model.SharedKernel;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeliveryApp.Core.Domain.Model.CounterAggegate;

public class Courier : Entity<Guid>
{
    #region Свойства

    /// </summary>
    public Location Location { get; init; }

    #endregion
        
    #region Constructor

    private Courier()
    { }

    public Courier(Location location) : this()
    {
        this.Id = Guid.NewGuid();
        this.Location = location ?? throw new ArgumentNullException(nameof(location), "Место доставки заказа не может быть пустым");

    }

    #endregion
}
