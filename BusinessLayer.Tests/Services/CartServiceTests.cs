using BusinessLayer.Services.Interfaces;
using TestUtilities.MockedObjects;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using BusinessLayer.Models.Cart.Requests;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Tests.Services;

public class CartServiceTests
{
    private MockedDependencyInjectionBuilder _serviceProviderBuilder;

    public CartServiceTests()
    {
        _serviceProviderBuilder = new MockedDependencyInjectionBuilder().AddMockedDBContext().AddServices();
    }

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
            OrderDate = null
        };

        // Act
        var created = await cartService.CreateAsync(createDto);

        // Assert
        Assert.NotNull(created);
        Assert.True(created.Id > 0, "Created cart should have a positive Id");
        Assert.Equal(createDto.UserId, created.UserId);
        Assert.Equal(createDto.TotalValue, created.TotalValue);

        // Verify persistence by fetching from service
        var fetched = await cartService.GetByIdAsync(created.Id);
        Assert.NotNull(fetched);
        Assert.Equal(created.Id, fetched!.Id);
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
            OrderId = null,
            OrderDate = null
        };

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(async () =>
        {
            await cartService.CreateAsync(createDto);
        });
    }
}