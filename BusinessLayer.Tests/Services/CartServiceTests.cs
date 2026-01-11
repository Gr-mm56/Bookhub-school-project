using BusinessLayer.Models.Cart.Requests;
using BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TestUtilities.MockedObjects;

namespace BusinessLayer.Tests.Services;

public class CartServiceTests
{
    private readonly MockedDependencyInjectionBuilder _serviceProviderBuilder = new MockedDependencyInjectionBuilder().AddServices().AddSeededDbContext();

    [Fact]
    public async Task CreateCart_ValidInput_ReturnsCartDto()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var cartService = provider.GetRequiredService<ICartService>();

        var createDto = new CartCreateDto
        {
            UserId = 1, // existing user from TestDataHelper
            TotalValue = 25.50,
            OrderId = null,
            OrderDate = null,
            PaymentStatus = 0
        };

        // Act
        var created = await cartService.CreateAsync(createDto);

        // Assert
        Assert.NotNull(created);
        Assert.True(created.Id > 0, "Created cart should have a positive Id");
        Assert.Equal(createDto.UserId, created.UserId);
        Assert.Equal(createDto.TotalValue, created.TotalValue);
        Assert.Equal(createDto.PaymentStatus, created.PaymentStatus);

        // Verify persistence by fetching from service
        var fetched = await cartService.GetByIdAsync(created.Id);
        Assert.NotNull(fetched);
        Assert.Equal(created.Id, fetched.Id);
        Assert.Equal(createDto.UserId, fetched.UserId);
        Assert.Equal(createDto.TotalValue, fetched.TotalValue);
    }
    [Fact]
    public async Task CreateCart_InvalidUserId_ThrowsException()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var cartService = provider.GetRequiredService<ICartService>();

        var createDto = new CartCreateDto
        {
            UserId = 9999, // non-existing user
            TotalValue = 30.00,
            OrderId = 54,
            OrderDate = null,
            PaymentStatus = 1
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await cartService.CreateAsync(createDto);
        });
    }
    [Fact]
    public async Task CreateCart_NegativeTotalValue_ThrowsException()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var cartService = provider.GetRequiredService<ICartService>();

        var createDto = new CartCreateDto
        {
            UserId = 1,
            TotalValue = -10.00,
            OrderId = null,
            OrderDate = null,
            PaymentStatus = 0
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await cartService.CreateAsync(createDto);
        });
    }
    [Fact]
    public async Task GetById_ExistingId_ReturnsCartDto()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var cartService = provider.GetRequiredService<ICartService>();

        var createDto = new CartCreateDto
        {
            UserId = 1,
            TotalValue = 20.00,
            OrderId = null,
            OrderDate = null,
            PaymentStatus = 0
        };

        var created = await cartService.CreateAsync(createDto);

        // Act
        var fetched = await cartService.GetByIdAsync(created.Id);

        // Assert
        Assert.NotNull(fetched);
        Assert.Equal(created.Id, fetched.Id);
        Assert.Equal(createDto.UserId, fetched.UserId);
        Assert.Equal(createDto.TotalValue, fetched.TotalValue);
    }
    [Fact]
    public async Task GetById_NonExistingId_ReturnsNull()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var cartService = provider.GetRequiredService<ICartService>();

        // Act
        var fetched = await cartService.GetByIdAsync(9999);

        // Assert
        Assert.Null(fetched);
    }
    [Fact]
    public async Task GetAllAsync_ReturnsPagedResult()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var cartService = provider.GetRequiredService<ICartService>();

        for (var i = 0; i < 5; i++)
        {
            var createDto = new CartCreateDto
            {
                UserId = 1,
                TotalValue = 10.00 + i,
                OrderId = null,
                OrderDate = null,
                PaymentStatus = 0
            };
            await cartService.CreateAsync(createDto);
        }

        // Act
        var pagedResult = await cartService.GetAllAsync(limit: 3, offset: 0);

        // Assert
        Assert.NotNull(pagedResult);
        Assert.Equal(3, pagedResult.Items.Count());
        Assert.True(pagedResult.Total >= 5);
    }

    [Fact]
    public async Task PutCart_ValidUpdate_ReturnsUpdatedCartDto()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var cartService = provider.GetRequiredService<ICartService>();

        var createDto = new CartCreateDto
        {
            UserId = 1,
            TotalValue = 20.00,
            OrderId = 1,
            OrderDate = null,
            PaymentStatus = 0
        };

        var created = await cartService.CreateAsync(createDto);

        var updateDto = new CartUpdateDto
        {
            TotalValue = 30.00,
            OrderId = 123,
            OrderDate = DateTime.UtcNow,
            PaymentStatus = 0
        };

        // Act
        var updated = await cartService.UpdateAsync(created.Id, updateDto);

        // Assert
        Assert.NotNull(updated);
        Assert.Equal(created.Id, updated.Id);
        Assert.Equal(updateDto.TotalValue, updated.TotalValue);
        Assert.Equal(updateDto.OrderId, updated.OrderId);
        Assert.Equal(updateDto.OrderDate, updated.OrderDate);
    }
    [Fact]
    public async Task PutCart_NonExistingOrderId_ReturnsNull()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var cartService = provider.GetRequiredService<ICartService>();

        var createDto = new CartCreateDto
        {
            UserId = 1,
            TotalValue = 20.00,
            OrderId = null,
            OrderDate = null,
            PaymentStatus = 0
        };

        await cartService.CreateAsync(createDto);

        var updateDto = new CartUpdateDto
        {
            TotalValue = 30.00,
            OrderId = 123,
            OrderDate = DateTime.UtcNow
        };

        // Act
        var updated = await cartService.UpdateAsync(9999, updateDto); // Non-existing cart ID

        // Assert
        Assert.Null(updated);
    }
    [Fact]
    public async Task PutCart_NegativeTotalValue_ReturnsNull()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var cartService = provider.GetRequiredService<ICartService>();

        var createDto = new CartCreateDto
        {
            UserId = 1,
            TotalValue = 20.00,
            OrderId = null,
            OrderDate = null,
            PaymentStatus = 0
        };

        var created = await cartService.CreateAsync(createDto);

        var updateDto = new CartUpdateDto
        {
            TotalValue = -5.00,
            OrderId = 1,
            OrderDate = DateTime.UtcNow
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await cartService.UpdateAsync(created.Id, updateDto);
        });
    }
    [Fact]
    public async Task DeleteCart_ExistingId_ReturnsTrue()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var cartService = provider.GetRequiredService<ICartService>();

        var createDto = new CartCreateDto
        {
            UserId = 1,
            TotalValue = 20.00,
            OrderId = null,
            OrderDate = null,
            PaymentStatus = 0
        };

        var created = await cartService.CreateAsync(createDto);

        // Act
        var deleted = await cartService.DeleteAsync(created.Id);

        // Assert
        Assert.True(deleted);

        var fetched = await cartService.GetByIdAsync(created.Id);
        Assert.Null(fetched);
    }

    [Fact]
    public async Task DeleteCart_NonExistingId_ReturnsFalse()
    {
        var provider = _serviceProviderBuilder.Create();
        var cartService = provider.GetRequiredService<ICartService>();
        // Act
        var deleted = await cartService.DeleteAsync(9999); // Non-existing cart ID
        // Assert
        Assert.False(deleted);
    }
}
