using DataAccessLayer.Context;
using DataAccessLayer.Entities;

namespace DataAccessLayer.Repository;

public class BookRepository: BaseRepository<Book>
{
    public BookRepository(BookHubDbContext context) : base(context)
    {
    }
}