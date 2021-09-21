using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimbirHomeworkClean.Application.Contracts.Repositories;
using SimbirHomeworkClean.Domain.Entities;
using SimbirHomeworkClean.Infrastructure.Data;
using SimbirHomeworkClean.Infrastructure.Repositories.Base;

namespace SimbirHomeworkClean.Infrastructure.Repositories
{
    // Пункт задания: 6
    /// <summary>
    /// Репозиторий жанров
    /// </summary>
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public GenreRepository(MainDbContext context) : base(context) { }

        /// <inheritdoc />
        public async Task<List<Genre>> GetListByGenreNamesAsync(IEnumerable<string> genreNames)
        {
            return await _context.Genre
                                 .Where(g => genreNames.Contains(g.GenreName))
                                 .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<List<(Genre, int)>> GetListByGenreNamesAsync()
        {
            return await _context.Genre
                                 .Select(g => new Tuple<Genre, int>(g, g.Books.Count).ToValueTuple())
                                 .ToListAsync();
        }
    }
}
