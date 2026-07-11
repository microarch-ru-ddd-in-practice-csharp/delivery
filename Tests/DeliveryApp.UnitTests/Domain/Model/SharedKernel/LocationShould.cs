using System;
using Xunit;
using DeliveryApp.Core.Domain.Model.SharedKernel;

namespace DeliveryApp.UnitTests.Domain.Model.SharedKernel;

public class LocationShould
{
    [Fact]
    public void BeCreateLocationWithValidCoordinates()
    {
        // Arrange
        int x = 5;
        int y = 7;
        // Act
        var location = new Location(x, y);
        // Assert
        Assert.Equal(x, location.X);
        Assert.Equal(y, location.Y);
    }

    [Fact]
    public void BeThrowArgumentExceptionForInvalidCoordinates()
    {
        // Arrange
        int invalidX = -1;
        int invalidY = 11;
        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Location(invalidX, 5));
        Assert.Throws<ArgumentException>(() => new Location(5, invalidY));
    }

    [Fact]
    public void BCalculateDistanceBetweenTwoLocations()
    {
        // Arrange
        var location1 = new Location(2, 3);
        var location2 = new Location(5, 7);
        // Act
        int distance = location1.Distance(location2);
        // Assert
        Assert.Equal(7, distance);
    }

    [Fact]
    public void BeEqualLocationsWithSameCoordinates()
    {
        // Arrange
        var location1 = new Location(4, 6);
        var location2 = new Location(4, 6);
        // Act & Assert
        Assert.Equal(location1, location2);
    }

    [Fact]
    public void NotBeEqualLocationsWithDifferentCoordinates()
    {
        // Arrange
        var location1 = new Location(1, 2);
        var location2 = new Location(3, 4);
        // Act & Assert
        Assert.NotEqual(location1, location2);
    }

    [Fact]
    public void BCreateRandomLocationWithinBounds()
    {
        // Act
        var randomLocation = Location.CreateRandom();
        // Assert
        Assert.InRange(randomLocation.X, 1, 10);
        Assert.InRange(randomLocation.Y, 1, 10);
    }
}
