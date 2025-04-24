using DeliveryApp.Core.Domain.Model.SharedKernel;
using FluentAssertions;
using Xunit;

namespace DeliveryApp.UnitTests.Domain.SharedKernel
{
    public class SpeedShould
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void BeCorrectWhenParamsIsCorrectOnCreated(byte speed)
        {
            //Arrange

            //Act
            var result = Speed.Create(speed);

            //Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Value.Should().Be(speed);
        }

        [Theory]
        [InlineData(Speed.Min - 1)]
        [InlineData(Speed.Max + 1)]
        public void ReturnErrorWhenParamsIsIncorrectOnCreated(byte speed)
        {
            //Arrange

            //Act
            var result = Speed.Create(speed);

            //Assert
            result.IsSuccess.Should().BeFalse();
            result.Error.Should().NotBeNull();
        }

        [Fact]

        public void BeEqualsWhenAllPropertiesAreEquals()
        {
            //Arrange
            var speed1 = Speed.Create(1).Value;
            var speed2 = Speed.Create(1).Value;

            //Act
            var result = speed1.Equals(speed2);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void NotBeEqualsWhenAllPropertiesAreNotEquals()
        {
            //Arrange
            var speed1 = Speed.Create(1).Value;
            var speed2 = Speed.Create(2).Value;

            //Act
            var result = speed1.Equals(speed2);

            //Assert
            result.Should().BeFalse();
        }
    }
}
