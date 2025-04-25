using CaffeTipping.DbServices.Services;
using CaffeTipping.ServicesContract;
using CaffeTipping.ServicesContract.Dtos;
using CaffeTipping.ServicesContract.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace CaffeTipping.DbServices.Tests;

public class OrderTipServiceTests
{
    private IOrderTipService _orderTipService = null!;
    
    [SetUp]
    public void Setup()
    {
        var serviceProvider = ServiceBuilder.GetServiceProvider();
        _orderTipService = serviceProvider.GetRequiredService<IOrderTipService>();
    }

    [Test]
    public async Task GetTipWhenNotInDatabase()
    {
        // Arrange
        var tableId = Guid.NewGuid();
        
        // Act 
        var orderTip = await _orderTipService.GetOrCreateOrderTip(tableId);
        
        // Assert
        Assert.IsNotNull(orderTip);
        Assert.That(tableId, Is.EqualTo(orderTip.TableId));
        Assert.That(orderTip.Bill, Is.Positive);
        Assert.That(orderTip.Id, Is.EqualTo(Guid.Empty));
    }
    
    [Test]
    public async Task GetTipWhenInDatabase()
    {
        // Arrange
        var tableId = Guid.NewGuid();
        var orderId = Guid.NewGuid();
        
        await _orderTipService.CreateOrderTip(new()
        {
            Id = orderId,
            TableId = tableId,
            Email = "test@test.com",
            Bill = 1234.5678,
            Tip = 10,
            Rating = 4
        });
        
        // Act 
        var orderTip = await _orderTipService.GetOrCreateOrderTip(tableId);
        
        // Assert
        Assert.IsNotNull(orderTip);
        Assert.That(tableId, Is.EqualTo(orderTip.TableId));
        Assert.That(orderTip.Bill, Is.Positive);
        Assert.That(orderTip.Id, Is.EqualTo(orderId));
    }

    [Test]
    public async Task CreateWithExistingTableId()
    {
        // Arrange
        var orderTip = new OrderTipDto()
        {
            Id = Guid.NewGuid(),
            TableId = Guid.NewGuid(),
            Email = "test@test.com",
            Bill = 1234.5678,
            Tip = 10,
            Rating = 4
        }; 

        // Act
        await _orderTipService.CreateOrderTip(orderTip);
        
        // Assert
        Assert.ThrowsAsync<DuplicateTableIdException>(async () => await _orderTipService.CreateOrderTip(orderTip));
    }
    
     
    [Test]
    public async Task CreateWithExistingId()
    {
        // Arrange
        var orderId = Guid.NewGuid();
        var orderTip = new OrderTipDto()
        {
            Id = orderId,
            TableId = Guid.NewGuid(),
            Email = "test@test.com",
            Bill = 1234.5678,
            Tip = 10,
            Rating = 4
        };
        
        var orderTip2 = new OrderTipDto()
        {
            Id = orderId,
            TableId = Guid.NewGuid(),
            Email = "test@test.com",
            Bill = 1234.5678,
            Tip = 10,
            Rating = 4
        };

        // Act
        await _orderTipService.CreateOrderTip(orderTip);
        
        // Assert
        Assert.ThrowsAsync<ArgumentException>(async () => await _orderTipService.CreateOrderTip(orderTip2));
    }
}