using DeliveryApp.Core.Domain.Model.AssignmentAggregate;
using Xunit;

namespace DeliveryApp.UnitTests.Domain.Model.AssignmentAggregate;

public class AssignmentStatusShould
{
    [Fact]
    public void BeEqualForSameStatus()
    {
        // Arrange
        var status1 = AssignmentStatus.Assigned;
        var status2 = AssignmentStatus.Assigned;
        // Act
        bool equal = status1 == status2;
        // Assert
        Assert.True(equal);
    }

    [Fact]
    public void NotBeEqualForDIfferentStatus()
    {
        // Arrange
        var status1 = AssignmentStatus.Assigned;
        var status2 = AssignmentStatus.Completed;
        // Act
        bool equal = status1 == status2;
        // Assert
        Assert.False(equal);
    }
}
