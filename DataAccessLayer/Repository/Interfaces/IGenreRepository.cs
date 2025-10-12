using DataAccessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLayer.Interfaces;

public interface IGenreRepository: IRepository<Genre>
{
    Task<IEnumerable<Genre>> SearchGenresAsync(string name);
    Task<Genre?> GetGenreWithBooksAsync(int id);
}