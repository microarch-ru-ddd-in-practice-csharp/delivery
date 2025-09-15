using System.Collections.Generic;
using DeliveryApp.Core.Domain.Model.SharedKernel;
using FluentAssertions;
using Xunit;

namespace DeliveryApp.UnitTests.Domain.Model.SharedKernel;

public class LocationShould
{
    public static IEnumerable<object[]> GetLocations()
    {
        yield return [Location.Create(1, 1).Value, 0];
        yield return [Location.Create(1, 2).Value, 1];        
    }

    [Fact]
    public void CorrectOnCreated()
    {        
        var location = Location.Create(1, 1);
        
        location.IsSuccess.Should().BeTrue();
        location.Value.X.Should().Be(1);
        location.Value.Y.Should().Be(1);
    }

    [Theory]
    [InlineData(0, 0)]
    [InlineData(-5, 1)]
    [InlineData(22, 1)]
    [InlineData(1, -6)]
    [InlineData(1, 20)]
    public void InCorrectOnCreated(int x, int y)
    {        
        var location = Location.Create(x, y);

        location.IsSuccess.Should().BeFalse();
        location.Error.Should().NotBeNull();
    }

    [Fact]
    public void CanCreateRandomLocation()
    {        
        var location = Location.CreateRandom();

        location.Should().NotBeNull();
        location.X.Should().BeGreaterThanOrEqualTo(1).And.BeLessThanOrEqualTo(10);
        location.Y.Should().BeGreaterThanOrEqualTo(1).And.BeLessThanOrEqualTo(10);
    }

    [Fact]
    public void AllPropertiesIsEqual()
    {
        var first = Location.Create(1, 1).Value;
        var second = Location.Create(1, 1).Value;

        var result = first == second;

        result.Should().BeTrue();
    }

    [Fact]
    public void AllPropertiesIsNotEqual()
    {
        var first = Location.Create(1, 1).Value;
        var second = Location.Create(10, 10).Value;

        var result = first == second;

        result.Should().BeFalse();
    }

    [Theory]
    [MemberData(nameof(GetLocations))]
    public void DistanceTwoLocations(Location anotherLocation, int distance)
    {
        var location = Location.Create(1, 1).Value;

        var result = location.DistanceTo(anotherLocation);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(distance);
    }
}
