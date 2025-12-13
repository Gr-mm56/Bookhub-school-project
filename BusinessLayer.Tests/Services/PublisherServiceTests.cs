using BusinessLayer.Models.Publisher.Requests;
using BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TestUtilities.MockedObjects;

namespace BusinessLayer.Tests.Services;

public class PublisherServiceTests
{
    private readonly MockedDependencyInjectionBuilder _serviceProviderBuilder
        = new MockedDependencyInjectionBuilder().AddServices().AddSeededDbContext();

    [Fact]
    public async Task CreatePublisher_ValidInput_ReturnsPublisherDto()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var publisherService = provider.GetRequiredService<IPublisherService>();

        var createDto = CreatePublisherDto();

        // Act
        var created = await publisherService.CreateAsync(createDto);

        // Assert
        Assert.NotNull(created);
        Assert.True(created.Id > 0, "Created publisher should have a positive Id");
        Assert.Equal(createDto.ProfilePhotoId, created.ProfilePhoto?.Id);
        Assert.Equal(createDto.Name, created.Name);
        Assert.Equal(createDto.Address, created.Address);
        Assert.Equal(createDto.BookIds, created.Books.Select(b => b.Id).ToList());

        // Verify persistence by fetching from service
        var fetched = await publisherService.GetByIdAsync(created.Id);

        Assert.NotNull(fetched);
        Assert.Equal(created.Id, fetched.Id);
        Assert.Equal(created.ProfilePhoto?.Id, fetched.ProfilePhoto?.Id);
        Assert.Equal(created.Name, fetched.Name);
        Assert.Equal(created.Address, fetched.Address);
    }

    [Fact]
    public async Task CreatePublisher_InvalidImageId_ThrowsException()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var publisherService = provider.GetRequiredService<IPublisherService>();

        var createDto = new PublisherRequestDto
        {
            Name = "HarperCollins",
            Address = "195 Broadway, New York, NY 10007, USA",
            ProfilePhotoId = -1,
            BookIds = new List<int>()
        };

        var created = await publisherService.CreateAsync(createDto);

        // Act & Assert
        Assert.Null(created.ProfilePhoto);
    }

    [Fact]
    public async Task GetById_ExistingId_ReturnsPublisherDto()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var publisherService = provider.GetRequiredService<IPublisherService>();

        var createDto = CreatePublisherDto();

        var created = await publisherService.CreateAsync(createDto);

        // Act
        var fetched = await publisherService.GetByIdAsync(created.Id);

        // Assert
        Assert.NotNull(fetched);
        Assert.True(fetched.Id > 0, "Created publisher should have a positive Id");
        Assert.Equal(fetched.ProfilePhoto?.Id, created.ProfilePhoto?.Id);
        Assert.Equal(fetched.Name, created.Name);
        Assert.Equal(fetched.Address, created.Address);
    }

    [Fact]
    public async Task GetById_NonExistingId_ReturnsNull()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var publisherService = provider.GetRequiredService<IPublisherService>();

        // Act
        var fetched = await publisherService.GetByIdAsync(9999);

        // Assert
        Assert.Null(fetched);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsPagedResult()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var publisherService = provider.GetRequiredService<IPublisherService>();

        for (var i = 0; i < 5; i++)
        {
            var createDto = new PublisherRequestDto
            {
                Name = "HarperCollins",
                Address = "195 Broadway, New York, NY 10007, USA",
                ProfilePhotoId = 1 + i,
                BookIds = new List<int>()
            };
            await publisherService.CreateAsync(createDto);
        }

        // Act
        var pagedResult = await publisherService.GetAllAsync(limit: 3, offset: 0);

        // Assert
        Assert.NotNull(pagedResult);
        Assert.Equal(3, pagedResult.Items.Count());
        Assert.True(pagedResult.Total >= 5);
    }

    [Fact]
    public async Task PutPublisher_ValidUpdate_ReturnsUpdatedPublisherDto()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var publisherService = provider.GetRequiredService<IPublisherService>();

        var createDto = CreatePublisherDto();

        var created = await publisherService.CreateAsync(createDto);

        var updateDto = new PublisherRequestDto
        {
            Name = "Updated Name",
            Address = "Updated Address"
        };

        // Act
        var updated = await publisherService.UpdateAsync(created.Id, updateDto);

        // Assert
        Assert.NotNull(updated);
        Assert.Equal(created.Id, updated.Id);
        Assert.Equal(updateDto.Name, updated.Name);
        Assert.Equal(updateDto.Address, updated.Address);
    }

    [Fact]
    public async Task PutPublisher_NonExistingImageId_ThrowsException()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var publisherService = provider.GetRequiredService<IPublisherService>();

        var createDto = CreatePublisherDto();
        var created = await publisherService.CreateAsync(createDto);

        var updateDto = new PublisherRequestDto
        {
            Name = "Updated Name",
            Address = "Updated Address",
            ProfilePhotoId = 15 // Non-existing Image ID
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
            await publisherService.UpdateAsync(created.Id, updateDto));

        Assert.Contains("Invalid Image ID", exception.Message);
    }

    [Fact]
    public async Task DeletePublisher_ExistingId_ReturnsTrue()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var publisherService = provider.GetRequiredService<IPublisherService>();

        var createDto = CreatePublisherDto();

        var created = await publisherService.CreateAsync(createDto);

        // Act
        var deleted = await publisherService.DeleteAsync(created.Id);

        // Assert
        Assert.True(deleted);

        var fetched = await publisherService.GetByIdAsync(created.Id);
        Assert.Null(fetched);
    }

    [Fact]
    public async Task DeletePublisher_NonExistingId_ReturnsFalse()
    {
        var provider = _serviceProviderBuilder.Create();
        var publisherService = provider.GetRequiredService<IPublisherService>();

        // Act
        var deleted = await publisherService.DeleteAsync(9999); // Non-existing publisher ID

        // Assert
        Assert.False(deleted);
    }

    private PublisherRequestDto CreatePublisherDto()
    {
        return new PublisherRequestDto
        {
            Name = "HarperCollins",
            Address = "195 Broadway, New York, NY 10007, USA",
            ProfilePhotoId = 1,
            BookIds = new List<int>()
        };
    }
}
