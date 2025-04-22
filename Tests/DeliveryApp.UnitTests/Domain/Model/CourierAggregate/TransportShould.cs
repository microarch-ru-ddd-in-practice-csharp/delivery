using DeliveryApp.Core.Domain.Model.SharedKernel;
using System.Collections.Generic;
using DeliveryApp.Core.Domain.Model.CourierAggregate;
using FluentAssertions;
using Xunit;

namespace DeliveryApp.UnitTests.Domain.Model.CourierAggregate
{
    public class TransportShould
    {
        public static IEnumerable<object[]> GetValidTransport()
        {
            yield return ["feet", Speed.Create(1).Value];
            yield return ["bicycle", Speed.Create(2).Value];
            yield return ["car", Speed.Create(3).Value];
        }

        public static IEnumerable<object[]> GetInvalidTransport()
        {
            yield return [null, Speed.Create(1).Value];
            yield return [string.Empty, Speed.Create(2).Value];
            yield return ["bicycle", null];
            yield return ["  ", Speed.Create(3).Value];
        }

        public static IEnumerable<object[]> GetTransportMovement()
        {
            yield return ["feet", Speed.Create(1).Value, Location.Create(2, 9).Value, Location.Create(9, 2).Value, 1, Location.Create(3, 9).Value];
            yield return ["feet", Speed.Create(1).Value, Location.Create(2, 9).Value, Location.Create(9, 2).Value, 8, Location.Create(9, 8).Value];
            yield return ["feet", Speed.Create(1).Value, Location.Create(2, 9).Value, Location.Create(9, 2).Value, 14, Location.Create(9, 2).Value];
            yield return ["feet", Speed.Create(1).Value, Location.Create(2, 9).Value, Location.Create(9, 2).Value, 20, Location.Create(9, 2).Value];
            yield return ["car", Speed.Create(3).Value, Location.Create(10, 3).Value, Location.Create(7, 8).Value, 1, Location.Create(7, 3).Value];
            yield return ["car", Speed.Create(3).Value, Location.Create(10, 3).Value, Location.Create(7, 8).Value, 2, Location.Create(7, 6).Value];
            yield return ["car", Speed.Create(3).Value, Location.Create(10, 3).Value, Location.Create(7, 8).Value, 3, Location.Create(7, 8).Value];
        }

        [Theory]
        [MemberData(nameof(GetValidTransport))]
        public void BeCorrectWhenParamsIsCorrectOnCreated(string name, Speed speed)
        {
            //Arrange

            //Act
            var result = Transport.Create(name, speed);

            //Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Name.Should().Be(name);
            result.Value.Speed.Should().Be(speed);
        }

        [Theory]
        [MemberData(nameof(GetInvalidTransport))]
        public void ReturnErrorWhenParamsIsIncorrectOnCreated(string name, Speed speed)
        {
            //Arrange

            //Act
            var result = Transport.Create(name, speed);

            //Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
        }

        [Fact]
        public void SetNameWhenNameIsCorrect()
        {
            var transport = Transport.Create("feeeeet", Speed.Create(1).Value).Value;
            transport.SetName("feet");

            transport.Name.Should().Be("feet");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void NotSetNameWhenNameIsIncorrect(string name)
        {
            var result = Transport.Create(name, Speed.Create(1).Value);

            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
        }

        [Fact]
        public void SetSpeed()
        {
            var transport = Transport.Create("feet", Speed.Create(1).Value).Value;
            var newSpeed = Speed.Create(2).Value;
            transport.SetSpeed(newSpeed);

            transport.Speed.Should().Be(newSpeed);
        }

        [Theory]
        [MemberData(nameof(GetTransportMovement))]
        public void ShouldMoveCorrect(string name, Speed speed, Location from, Location to, int steps, Location expected)
        {
            var transport = Transport.Create(name, speed).Value;
            var result = from;
            while (steps > 0)
            {
                result = transport.Move(result, to);
                steps--;
            }

            result.Should().Be(expected);
        }
    }
}
