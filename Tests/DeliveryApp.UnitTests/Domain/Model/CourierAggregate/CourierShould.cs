using DeliveryApp.Core.Domain.Model.AssignmentAggregate;
using DeliveryApp.Core.Domain.Model.CounterAggegate;
using DeliveryApp.Core.Domain.Model.OrderAggegate;
using DeliveryApp.Core.Domain.Model.SharedKernel;
using System;
using Xunit;

namespace DeliveryApp.UnitTests.Domain.Model.CourierAggregate
{
    public class CourierShould
    {
        private Order Order1 = new Order(Guid.NewGuid(), new Location(1, 5), new Volume(15));
        private Order Order2 = new Order(Guid.NewGuid(), new Location(1, 5), new Volume(6));
        [Fact]
        public void ShouldCreateCourierWithValidParameters()
        {
            // Arrange
            var location = new Location(1, 5);
            var name = "Иван Иваныч Иванов";
            // Act
            var courier = new Courier(name, location);
            // Assert
            Assert.Equal(name, courier.Name);
            Assert.Equal(location, courier.Location);
            Assert.Equal(20, courier.MaxVolume.Capatity);
        }

        [Fact]
        public void ShouldThrowArgumentExceptionWhenInvalideParameters()
        {
            // Arrange
            var location = new Location(1, 5);
            // Act & Assert
            Assert.Throws<ArgumentException>(() => new Courier(string.Empty, location));
            Assert.Throws<ArgumentException>(() => new Courier(null, location));
            Assert.Throws<ArgumentNullException>(() => new Courier("Иван Ивановмч", null));
        }

        [Fact]
        public void ShouldThrowCourierInvalideLocationExceptionWhenLocationDistanceMoreThanOne()
        {
            // Arrange
            var location = new Location(1, 5);
            var courier = new Courier("Иван Ивановмч", location);
            var newLocation = new Location(3, 5);
            // Act & Assert
            Assert.Throws<Courier.CourierInvalideLocationException>(() => courier.Location = newLocation);
        }

        [Fact]
        public void ShouldAddAssignmentWhenCanAddAssignmentIsTrue()
        {
            // Arrange
            var location = new Location(1, 5);
            var courier = new Courier("Иван Ивановмч", location);
            var assignment = new Assignment(Order1);
            // Act
            courier.AddAssignment(assignment);
            // Assert
            Assert.Contains(assignment, courier.Assignments);
            Assert.Equal(assignment.OrderId, Order1.Id);
        }

        [Fact]
        public void ShouldThrowCourierMaxVolumeExceededExceptionIfTuiMatchVolume()
        { 
            // Arrange
            var location = new Location(1, 5);
            var courier = new Courier("Иван Ивановмч", location);

            var assignment1 = new Assignment(Order1);
            var assignment2 = new Assignment(Order2);
            // Act
            courier.AddAssignment(assignment1);

            // Assert
            Assert.Throws<Courier.CourierMaxVolumeExceededException>(() => courier.AddAssignment(assignment2));
        }

        [Fact]
        public void ShouldThrowAssignmentCourierNotSameLocationExceptionIfLocationIsNotSame()
        {
            // Arrange
            var location = new Location(1, 5);
            var courier = new Courier("Иван Ивановмч", location);

            var assignment1 = new Assignment(Order1);
            
            // Act
            courier.AddAssignment(assignment1);

            // Assert
            Assert.Throws<Assignment.AssignmentCourierNotSameLocationException>(() => courier.CompliteAssigment(assignment1, new Location(7,7)));
        }

        [Fact]
        public void ShouldMoveToNewLocation()
        {
            // Arrange
            var location = new Location(1, 5);
            var newlocation = new Location(1, 6);
            var courier = new Courier("Иван Ивановмч", location);

            var assignment = new Assignment(Order1);
            // Act
            courier.AddAssignment(assignment);
            courier.Location = newlocation;
            // Assert
            Assert.Contains(assignment, courier.Assignments);
            Assert.Equal(newlocation, courier.Location);
        }

        [Fact]
        public void ShouldThrowCourierInvalideLocationExceptionToMoveInvalideLocation()
        {
            // Arrange
            var location = new Location(1, 5);
            var newlocation = new Location(7, 6);
            var courier = new Courier("Иван Ивановмч", location);

            var assignment = new Assignment(Order1);
            // Act
            courier.AddAssignment(assignment);
            // Assert
            Assert.Throws<Courier.CourierInvalideLocationException>(() => courier.Location = newlocation);
        }
    }
}
