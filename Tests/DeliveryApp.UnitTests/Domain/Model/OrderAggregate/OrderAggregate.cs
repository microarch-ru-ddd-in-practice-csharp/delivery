using DeliveryApp.Core.Domain.Model.OrderAggregate;
using FluentAssertions;
using Xunit;

namespace DeliveryApp.UnitTests.Domain.Model.OrderAggregate;

public class OrderStatusShould
{
    [Fact]
    public void ReturnCorrectName()
    {        
        OrderStatus.Created.Name.Should().Be("created");
        OrderStatus.Assigned.Name.Should().Be("assigned");
        OrderStatus.Completed.Name.Should().Be("completed");
    }

    [Fact]
    public void BeEqualWhenAllPropertiesIsEqual()
    {
        var result = OrderStatus.Created == OrderStatus.Created;
        result.Should().BeTrue();
    }

    [Fact]
    public void BeNotEqualWhenAllPropertiesIsEqual()
    {        
        var result = OrderStatus.Created == OrderStatus.Completed;
        result.Should().BeFalse();
    }
}
