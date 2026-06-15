using DeliveryApp.Core.Domain.Models;
using FluentAssertions;
using Xunit;

namespace DeliveryApp.UnitTests.Domain.Model.SharedKernel;

public class LocationShould
{
    [Fact]
    public void ReturnCorrectLocation()
    {
        var location = Location.Create(5, 5);

        location.IsSuccess.Should().BeTrue();
        location.Value.X.Should().Be(5);
        location.Value.Y.Should().Be(5);
    }

    [Theory]
    [InlineData(0, 5)]
    [InlineData(-1, 5)]
    [InlineData(1, 15)]
    [InlineData(2, -15)]
    [InlineData(11, 5)]
    public void CreateWithValueOutOfRangeReturnsFailure(int xValue, int yValue)
    {
        var location = Location.Create(xValue, yValue);
        
        location.IsSuccess.Should().BeFalse();
        location.Error.Code.Should().Be("value.must.be.between");
    }
    
    [Fact]
    public void BeEqualLocationWhenEqual()
    {
        var location1 = Location.Create(5, 5);
        var location2 = Location.Create(5, 5);

        var result = location1.Equals(location2);
        result.Should().BeTrue();
    }

    [Fact]
    public void BeNotEqualLocationWhenNotEqual()
    {
        var location1 = Location.Create(5, 5);
        var location2 = Location.Create(6, 6);
        var result = location2.Equals(location1);
        
        result.Should().BeFalse();
    }

    [Fact]
    public void DistanceToReturnsValidDistance()
    {
        var location = Location.Create(5, 5).Value;
        var target = Location.Create(6, 6).Value;
        var distance = location.DistanceTo(target);
        
        distance.IsSuccess.Should().BeTrue();
        distance.Value.Should().Be(2);
    }

    [Fact]
    public void DistanceToBetweenExampleFromTaskReturnsFive()
    {
        var location = Location.Create(2, 3).Value;
        var target = Location.Create(4, 6).Value;
        var distance = location.DistanceTo(target);
        
        distance.IsSuccess.Should().BeTrue();
        distance.Value.Should().Be(5);
    }
    
    [Fact]
    public void DistanceToSelfReturnZero()
    {
        var location = Location.Create(5, 5).Value;
        var target = Location.Create(5, 5).Value;
        var distance = location.DistanceTo(target);
        
        distance.IsSuccess.Should().BeTrue();
        distance.Value.Should().Be(0);
    }

    [Fact]
    public void DistanceToWithNullTargetReturnsFailure()
    {
        var location = Location.Create(5, 5).Value;
        var distance = location.DistanceTo(null!);
        
        distance.IsSuccess.Should().BeFalse();
        distance.Error.Code.Should().Be("value.must.be.provided");
    }
    
}