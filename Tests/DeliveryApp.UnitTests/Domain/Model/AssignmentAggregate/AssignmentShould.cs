using DeliveryApp.Core.Domain.Model.AssignmentAggregate;
using DeliveryApp.Core.Domain.Model.SharedKernel;
using System;
using Xunit;
using static DeliveryApp.Core.Domain.Model.AssignmentAggregate.Assignment;

namespace DeliveryApp.UnitTests.Domain.Model.AssignmentAggregate;

public class AssignmentShould
{
    [Fact]
    public void BeCreateAssignmentWithValidProperties()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var status = AssignmentStatus.Assigned;
        var volume = new Volume(1);
        var location = new Location(1, 5);
        // Act
        var assignment = new Assignment(orderId, volume, location, AssignmentStatus.Assigned);
        // Assert
        Assert.Equal(orderId, assignment.OrderId);
        Assert.Equal(location, assignment.Location);
        Assert.Equal(status, assignment.Status);
    }

    [Fact]
    public void BeThrowArgumentNullExceptionForNullLocation()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var volume = new Volume(1);
        Location location = null;
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new Assignment(orderId, volume, location, AssignmentStatus.Assigned));
    }

    [Fact]
    public void BeThrowArgumentNullExceptionForNullVolume()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        Volume volume = null;
        var location = new Location(1, 5);
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new Assignment(orderId, volume, location, AssignmentStatus.Assigned));
    }

    [Fact]
    public void BeCompliteAssignment ()
    {

        // Arrange
        var orderId = Guid.NewGuid();
        var status = AssignmentStatus.Assigned;
        var volume = new Volume(1);
        var location = new Location(1, 5);
        var assignment = new Assignment(orderId, volume, location, AssignmentStatus.Assigned);
        // Act
        var exception = Record.Exception(() => assignment.Complete(location));
        // Assert
        Assert.Null(exception);
    }


    [Fact]
    public void BeThrowAssignmentCourierNotSameLocationExceptionOfCompliteAssignment()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var status = AssignmentStatus.Assigned;
        var volume = new Volume(1);
        var location = new Location(1, 5);
        var assignment = new Assignment(orderId, volume, location, AssignmentStatus.Assigned);
        // Act && Assert
        
        Assert.Throws<AssignmentCourierNotSameLocationException>(() => assignment.Complete(new Location(5, 5)));
    }

    [Fact]
    public void BeThrowAssignmentAlreadyCompletedExceptionOfCompliteAssignment()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var status = AssignmentStatus.Assigned;
        var volume = new Volume(1);
        var location = new Location(1, 5);
        var assignment = new Assignment(orderId, volume, location, AssignmentStatus.Assigned);
        // Act 
        assignment.Complete(new Location(1, 5));
        // Assert
        Assert.Throws<AssignmentAlreadyCompletedException>(() => assignment.Complete(new Location(1, 5)));
    }
}
