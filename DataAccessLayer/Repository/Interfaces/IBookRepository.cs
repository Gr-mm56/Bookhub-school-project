using DataAccessLayer.Entities;

namespace DataAccessLayer.Interfaces;

public interface IBookRepository: IRepository<Book>
{
    Task<IEnumerable<Book>> SearchBooksAsync(string? title, string? description, string? author, string? genre, string? publisher, double? price);
}