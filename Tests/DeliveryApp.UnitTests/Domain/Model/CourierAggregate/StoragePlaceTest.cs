using System;
using System.Linq;
using System.Reflection;
using CSharpFunctionalExtensions;
using FluentAssertions;
using Xunit;

namespace DeliveryApp.UnitTests.Domain.Model.CourierAggregate;

public class StoragePlaceShould
{
    [Fact]
    public void DerivedEntity()
    {        
        var isDerivedEntity = typeof(StoragePlace).IsSubclassOf(typeof(Entity<Guid>));  
              
        isDerivedEntity.Should().BeTrue();
    }
    
    [Fact]
    public void ConstructorShouldBePrivate()
    {        
        var typeInfo = typeof(StoragePlace).GetTypeInfo();

        typeInfo.DeclaredConstructors.All(x => x.IsPrivate).Should().BeTrue();
    }

    [Fact]
    public void BeCorrectWhenParamsAreCorrect()
    {       
        var result = StoragePlace.Create("Рюкзак", 5);

        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().NotBeEmpty();
        result.Value.Name.Should().Be("Рюкзак");
    }
    
    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(9)]
    public void CanStore(int volume)
    {        
        var storagePlaceCreateResult = StoragePlace.Create("Рюкзак", 5);
        storagePlaceCreateResult.IsSuccess.Should().BeTrue(); 
        
        var storagePlace = storagePlaceCreateResult.Value;

        var storagePlaceCanStoreResult = storagePlace.CanStore(volume);
        storagePlaceCanStoreResult.IsSuccess.Should().BeTrue();
        storagePlaceCanStoreResult.Value.Should().BeTrue();
    }
}
