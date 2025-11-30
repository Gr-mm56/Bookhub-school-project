using BusinessLayer.Services.Interfaces;
using TestUtilities.MockedObjects;
using Microsoft.Extensions.DependencyInjection;
using BusinessLayer.Models.Book.Requests;

namespace BusinessLayer.Tests.Services;

public class BookServiceTests
{
    private readonly MockedDependencyInjectionBuilder _serviceProviderBuilder = new MockedDependencyInjectionBuilder().AddServices().AddSeededDbContext();

    [Fact]
    public async Task CreateBook_ValidInput_ReturnsBookDto()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var bookService = provider.GetRequiredService<IBookService>();

        var createDto = new BookRequestDto
        {
            Title = "Test Book Title",
            ISBN = "978-3-16-148410-0",
            Description = "A test book description",
            Price = 25.99,
            ImageId = 1,
            PublisherId = 1,
            PrimaryGenreId = 2,
            GenreIds = [1],
            AuthorIds = [1]
        };

        // Act
        var created = await bookService.CreateAsync(createDto);

        // Assert
        Assert.NotNull(created);
        Assert.True(created.Id > 0, "Created book should have a positive Id");
        Assert.Equal(createDto.Title, created.Title);
        Assert.Equal(createDto.Price, created.Price);
        Assert.NotNull(created.Image);
        Assert.Equal(createDto.ImageId, created.Image.Id);

        // Verify persistence by fetching from service
        var fetched = await bookService.GetByIdAsync(created.Id);
        Assert.NotNull(fetched);
        Assert.Equal(created.Id, fetched.Id);
        Assert.Equal(createDto.Title, fetched.Title);
        Assert.Equal(createDto.ISBN, fetched.ISBN);
        Assert.Equal(createDto.Price, fetched.Price);
    }

    [Fact]
    public async Task CreateBook_NoGenres_ThrowsException()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var bookService = provider.GetRequiredService<IBookService>();

        var createDto = new BookRequestDto
        {
            Title = "Test Book",
            ISBN = "978-3-16-148410-0",
            Price = 25.99,
            ImageId = 1,
            PublisherId = 1,
            PrimaryGenreId = 2,
            GenreIds = [],
            AuthorIds = [1]
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await bookService.CreateAsync(createDto);
        });
    }

    [Fact]
    public async Task CreateBook_NoAuthors_ThrowsException()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var bookService = provider.GetRequiredService<IBookService>();

        var createDto = new BookRequestDto
        {
            Title = "Test Book",
            ISBN = "978-3-16-148410-0",
            Price = 25.99,
            ImageId = 1,
            PublisherId = 1,
            PrimaryGenreId = 2,
            GenreIds = [],
            AuthorIds = [1]
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await bookService.CreateAsync(createDto);
        });
    }

    [Fact]
    public async Task CreateBook_InvalidAuthorId_ThrowsException()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var bookService = provider.GetRequiredService<IBookService>();

        var createDto = new BookRequestDto
        {
            Title = "Test Book",
            ISBN = "978-3-16-148410-0",
            Price = 25.99,
            ImageId = 1,
            PublisherId = 1,
            PrimaryGenreId = 2,
            GenreIds = [1],
            AuthorIds = [9999]
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await bookService.CreateAsync(createDto);
        });
    }

    [Fact]
    public async Task CreateBook_InvalidGenreId_ThrowsException()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var bookService = provider.GetRequiredService<IBookService>();

        var createDto = new BookRequestDto
        {
            Title = "Test Book",
            ISBN = "978-3-16-148410-0",
            Price = 25.99,
            ImageId = 1,
            PublisherId = 1,
            PrimaryGenreId = 2,
            GenreIds = [9999],
            AuthorIds = [1]
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await bookService.CreateAsync(createDto);
        });
    }

    [Fact]
    public async Task CreateBook_InvalidPublisherId_ThrowsException()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var bookService = provider.GetRequiredService<IBookService>();

        var createDto = new BookRequestDto
        {
            Title = "Test Book",
            ISBN = "978-3-16-148410-0",
            Price = 25.99,
            ImageId = 1,
            PublisherId = 9999,
            PrimaryGenreId = 2,
            GenreIds = [1],
            AuthorIds = [1]
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await bookService.CreateAsync(createDto);
        });
    }

    [Fact]
    public async Task CreateBook_InvalidImageId_ThrowsException()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var bookService = provider.GetRequiredService<IBookService>();

        var createDto = new BookRequestDto
        {
            Title = "Test Book",
            ISBN = "978-3-16-148410-0",
            Price = 25.99,
            ImageId = 9999,
            PublisherId = 1,
            PrimaryGenreId = 2,
            GenreIds = [1],
            AuthorIds = [1]
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await bookService.CreateAsync(createDto);
        });
    }

    [Fact]
    public async Task GetById_ExistingId_ReturnsBookDetailDto()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var bookService = provider.GetRequiredService<IBookService>();

        var createDto = new BookRequestDto
        {
            Title = "Test Book",
            ISBN = "978-3-16-148410-0",
            Description = "Test description",
            Price = 29.99,
            ImageId = 1,
            PublisherId = 1,
            PrimaryGenreId = 2,
            GenreIds = [1],
            AuthorIds = [1]
        };

        var created = await bookService.CreateAsync(createDto);

        // Act
        var fetched = await bookService.GetByIdAsync(created.Id);

        // Assert
        Assert.NotNull(fetched);
        Assert.Equal(created.Id, fetched.Id);
        Assert.Equal(createDto.Title, fetched.Title);
        Assert.Equal(createDto.ISBN, fetched.ISBN);
        Assert.Equal(createDto.Description, fetched.Description);
        Assert.Equal(createDto.Price, fetched.Price);
        Assert.NotNull(fetched.Authors);
        Assert.NotEmpty(fetched.Authors);
        Assert.NotNull(fetched.Genres);
        Assert.NotEmpty(fetched.Genres);
        Assert.NotNull(fetched.Publisher);
        Assert.NotNull(fetched.Image);
    }

    [Fact]
    public async Task GetById_NonExistingId_ReturnsNull()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var bookService = provider.GetRequiredService<IBookService>();

        // Act
        var fetched = await bookService.GetByIdAsync(9999);

        // Assert
        Assert.Null(fetched);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsPagedResult()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var bookService = provider.GetRequiredService<IBookService>();

        for (var i = 0; i < 5; i++)
        {
            var createDto = new BookRequestDto
            {
                Title = $"Test Book {i}",
                ISBN = $"978-3-16-14841{i}-{i}",
                Price = 10.00 + i,
                ImageId = 1,
                PublisherId = 1,
                PrimaryGenreId = 2,
                GenreIds = [1],
                AuthorIds = [1]
            };
            await bookService.CreateAsync(createDto);
        }

        // Act
        var pagedResult = await bookService.GetAllAsync(limit: 3, offset: 0);

        // Assert
        Assert.NotNull(pagedResult);
        Assert.Equal(3, pagedResult.Items.Count());
        Assert.True(pagedResult.Total >= 5);
    }

    [Fact]
    public async Task UpdateBook_ValidUpdate_ReturnsUpdatedBookDto()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var bookService = provider.GetRequiredService<IBookService>();

        var createDto = new BookRequestDto
        {
            Title = "Original Title",
            ISBN = "978-3-16-148410-0",
            Description = "Original description",
            Price = 20.00,
            ImageId = 1,
            PublisherId = 1,
            PrimaryGenreId = 2,
            GenreIds = [1],
            AuthorIds = [1]
        };

        var created = await bookService.CreateAsync(createDto);

        var updateDto = new BookRequestDto
        {
            Title = "Updated Title",
            ISBN = "978-3-16-148410-1",
            Description = "Updated description",
            Price = 30.00,
            ImageId = 1,
            PublisherId = 1,
            PrimaryGenreId = 2,
            GenreIds = [1],
            AuthorIds = [1]
        };

        // Act
        var updated = await bookService.UpdateAsync(created.Id, updateDto);

        // Assert
        Assert.NotNull(updated);
        Assert.Equal(created.Id, updated.Id);
        Assert.Equal(updateDto.Title, updated.Title);
        Assert.Equal(updateDto.Price, updated.Price);
    }

    [Fact]
    public async Task UpdateBook_NonExistingId_ReturnsNull()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var bookService = provider.GetRequiredService<IBookService>();

        var updateDto = new BookRequestDto
        {
            Title = "Updated Title",
            ISBN = "978-3-16-148410-1",
            Price = 30.00,
            ImageId = 1,
            PublisherId = 1,
            PrimaryGenreId = 2,
            GenreIds = [1],
            AuthorIds = [1]
        };

        // Act
        var updated = await bookService.UpdateAsync(9999, updateDto);

        // Assert
        Assert.Null(updated);
    }

    [Fact]
    public async Task UpdateBook_InvalidRelatedEntities_ThrowsException()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var bookService = provider.GetRequiredService<IBookService>();

        var createDto = new BookRequestDto
        {
            Title = "Original Title",
            ISBN = "978-3-16-148410-0",
            Price = 20.00,
            ImageId = 1,
            PublisherId = 1,
            PrimaryGenreId = 2,
            GenreIds = [1],
            AuthorIds = [1]
        };

        var created = await bookService.CreateAsync(createDto);

        var updateDto = new BookRequestDto
        {
            Title = "Updated Title",
            ISBN = "978-3-16-148410-1",
            Price = 30.00,
            ImageId = 9999,
            PublisherId = 1,
            PrimaryGenreId = 2,
            GenreIds = [1],
            AuthorIds = [1]
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await bookService.UpdateAsync(created.Id, updateDto);
        });
    }

    [Fact]
    public async Task DeleteBook_ExistingId_ReturnsTrue()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var bookService = provider.GetRequiredService<IBookService>();

        var createDto = new BookRequestDto
        {
            Title = "Book to Delete",
            ISBN = "978-3-16-148410-0",
            Price = 15.00,
            ImageId = 1,
            PublisherId = 1,
            PrimaryGenreId = 2,
            GenreIds = [1],
            AuthorIds = [1]
        };

        var created = await bookService.CreateAsync(createDto);

        // Act
        var deleted = await bookService.DeleteAsync(created.Id);

        // Assert
        Assert.True(deleted);

        var fetched = await bookService.GetByIdAsync(created.Id);
        Assert.Null(fetched);
    }

    [Fact]
    public async Task DeleteBook_NonExistingId_ReturnsFalse()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var bookService = provider.GetRequiredService<IBookService>();

        // Act
        var deleted = await bookService.DeleteAsync(9999);

        // Assert
        Assert.False(deleted);
    }

    [Fact]
    public async Task SearchBooks_ByTitle_ReturnsMatchingBooks()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var bookService = provider.GetRequiredService<IBookService>();

        // Create test books with different titles
        var book1 = new BookRequestDto
        {
            Title = "Advanced Programming",
            ISBN = "978-3-16-148410-1",
            Price = 45.00,
            ImageId = 1,
            PublisherId = 1,
            PrimaryGenreId = 2,
            GenreIds = [1],
            AuthorIds = [1]
        };

        var book2 = new BookRequestDto
        {
            Title = "Basic Programming",
            ISBN = "978-3-16-148410-2",
            Price = 25.00,
            ImageId = 1,
            PublisherId = 1,
            PrimaryGenreId = 2,
            GenreIds = [1],
            AuthorIds = [1]
        };

        await bookService.CreateAsync(book1);
        await bookService.CreateAsync(book2);

        var searchDto = new BookSearchDto
        {
            SearchTerm = "Programming"
        };

        // Act
        var searchResult = await bookService.SearchBooksAsync(searchDto);

        // Assert
        Assert.NotNull(searchResult);
        Assert.True(searchResult.Items.Count() >= 2);
        Assert.All(searchResult.Items, item => Assert.Contains("Programming", item.Title));
    }

    [Fact]
    public async Task SearchBooks_ByPrice_ReturnsMatchingBooks()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var bookService = provider.GetRequiredService<IBookService>();

        var testPrice = 35.99;
        var createDto = new BookRequestDto
        {
            Title = "Specific Price Book",
            ISBN = "978-3-16-148410-3",
            Price = testPrice,
            ImageId = 1,
            PublisherId = 1,
            PrimaryGenreId = 2,
            GenreIds = [1],
            AuthorIds = [1]
        };

        var created = await bookService.CreateAsync(createDto);

        var searchDto = new BookSearchDto
        {
            Price = testPrice,
        };

        // Act
        var searchResult = await bookService.SearchBooksAsync(searchDto);

        // Assert
        Assert.NotNull(searchResult);
        Assert.Contains(searchResult.Items, item => item.Id == created.Id && Math.Abs(item.Price - testPrice) < 0.0001);
    }

    [Fact]
    public async Task SearchBooks_ByPublisher_ReturnsMatchingBooks()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var bookService = provider.GetRequiredService<IBookService>();

        var searchDto = new BookSearchDto
        {
            SearchTerm = "HarperCollins"
        };

        // Act
        var searchResult = await bookService.SearchBooksAsync(searchDto);

        // Assert
        Assert.NotNull(searchResult);
        Assert.NotEmpty(searchResult.Items);
    }

    [Fact]
    public async Task CreateBook_WithoutPublisher_CreatesSuccessfully()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var bookService = provider.GetRequiredService<IBookService>();

        var createDto = new BookRequestDto
        {
            Title = "Test Book No Publisher",
            ISBN = "978-3-16-148410-0",
            Description = "A test book without publisher",
            Price = 25.99,
            ImageId = 1,
            PublisherId = 0,
            PrimaryGenreId = 2,
            GenreIds = [1],
            AuthorIds = [1]
        };

        // Act
        var created = await bookService.CreateAsync(createDto);

        // Assert
        Assert.NotNull(created);
        Assert.True(created.Id > 0, "Created book should have a positive Id");
        Assert.Equal(createDto.Title, created.Title);
        Assert.Equal(createDto.Price, created.Price);
        Assert.NotNull(created.Image);
        Assert.Equal(createDto.ImageId, created.Image.Id);

        // Verify persistence by fetching from service
        var fetched = await bookService.GetByIdAsync(created.Id);
        Assert.NotNull(fetched);
        Assert.Equal(created.Id, fetched.Id);
        Assert.Equal(createDto.Title, fetched.Title);
        Assert.Equal(createDto.ISBN, fetched.ISBN);
        Assert.Equal(createDto.Price, fetched.Price);
        Assert.Null(fetched.Publisher);
    }

    [Fact]
    public async Task CreateBook_WithoutImage_CreatesSuccessfully()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var bookService = provider.GetRequiredService<IBookService>();

        var createDto = new BookRequestDto
        {
            Title = "Test Book No Image",
            ISBN = "978-3-16-148410-0",
            Description = "A test book without image",
            Price = 25.99,
            ImageId = 0,
            PublisherId = 1,
            PrimaryGenreId = 2,
            GenreIds = [1],
            AuthorIds = [1]
        };

        // Act
        var created = await bookService.CreateAsync(createDto);

        // Assert
        Assert.NotNull(created);
        Assert.True(created.Id > 0, "Created book should have a positive Id");
        Assert.Equal(createDto.Title, created.Title);
        Assert.Equal(createDto.Price, created.Price);

        // Verify persistence by fetching from service
        var fetched = await bookService.GetByIdAsync(created.Id);
        Assert.NotNull(fetched);
        Assert.Equal(created.Id, fetched.Id);
        Assert.Equal(createDto.Title, fetched.Title);
        Assert.Equal(createDto.ISBN, fetched.ISBN);
        Assert.Equal(createDto.Price, fetched.Price);
        Assert.Null(fetched.Image);
    }

    [Fact]
    public async Task CreateBook_MultipleInvalidIds_ThrowsExceptionWithAllErrors()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var bookService = provider.GetRequiredService<IBookService>();

        var createDto = new BookRequestDto
        {
            Title = "Test Book",
            ISBN = "978-3-16-148410-0",
            Price = 25.99,
            ImageId = 9999,
            PublisherId = 9999,
            PrimaryGenreId = 2,
            GenreIds = [9999],
            AuthorIds = [9999]
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await bookService.CreateAsync(createDto);
        });

        // Should contain all validation errors
        Assert.Contains("Invalid Author IDs", exception.Message);
        Assert.Contains("Invalid Genre IDs", exception.Message);
        Assert.Contains("Invalid Publisher ID", exception.Message);
        Assert.Contains("Invalid Image ID", exception.Message);
    }

    [Fact]
    public async Task SearchBooks_ByGenre_ReturnsMatchingBooks()
    {
        var provider = _serviceProviderBuilder.Create();
        var bookService = provider.GetRequiredService<IBookService>();

        var searchDto = new BookSearchDto
        {
            SearchTerm = "Science"
        };

        // Act
        var result = await bookService.SearchBooksAsync(searchDto);

        // Assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task UpdateBook_ChangeAuthorAndGenre_UpdatesCorrectly()
    {
        // Arrange
        var provider = _serviceProviderBuilder.Create();
        var bookService = provider.GetRequiredService<IBookService>();

        var createDto = new BookRequestDto
        {
            Title = "Update Test Book",
            ISBN = "978-3-16-148410-0",
            Price = 20.00,
            ImageId = 1,
            PublisherId = 1,
            PrimaryGenreId = 2,
            GenreIds = [1],
            AuthorIds = [1]
        };

        var created = await bookService.CreateAsync(createDto);

        var updateDto = new BookRequestDto
        {
            Title = "Updated Book",
            ISBN = "978-3-16-148410-1",
            Price = 30.00,
            ImageId = 1,
            PublisherId = 1,
            PrimaryGenreId = 2,
            GenreIds = [1, 2],
            AuthorIds = [1, 2]
        };

        // Act
        var updated = await bookService.UpdateAsync(created.Id, updateDto);

        // Assert
        Assert.NotNull(updated);
        var detailed = await bookService.GetByIdAsync(updated.Id);
        Assert.NotNull(detailed);
    }

}
