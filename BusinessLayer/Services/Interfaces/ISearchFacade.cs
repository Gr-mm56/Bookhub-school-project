using BusinessLayer.Models.Search.Responses;

namespace BusinessLayer.Services.Interfaces;

public interface ISearchFacade
{
    Task<SearchResultDto> SearchAsync(
     string? searchTerm = null,
     int bookPageNumber = 1,
     int authorPageNumber = 1,
     int publisherPageNumber = 1);
}

