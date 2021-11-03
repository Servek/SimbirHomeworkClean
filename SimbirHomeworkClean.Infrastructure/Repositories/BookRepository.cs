using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimbirHomeworkClean.Application.Contracts.Repositories;
using SimbirHomeworkClean.Application.Filters;
using SimbirHomeworkClean.Domain.Entities;
using SimbirHomeworkClean.Infrastructure.Data;
using SimbirHomeworkClean.Infrastructure.Repositories.Base;

namespace SimbirHomeworkClean.Infrastructure.Repositories
{
    // Пункт задания: 6
    /// <summary>
    /// Репозиторий книг
    /// </summary>
    public class BookRepository : Repository<Book>, IBookRepository
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public BookRepository(MainDbContext context) : base(context) { }

        /// <inheritdoc />
        public async Task<List<Book>> GetFilteredListAsync(BookFilter filter)
        {
            if (filter == null)
                throw new NullReferenceException();

            var books = _context.Book
                                .Include(b => b.Author)
                                .Include(b => b.Genres)
                                .AsNoTracking();

            if (!string.IsNullOrEmpty(filter.AuthorFirstName))
                books = books.Where(b => b.Author.FirstName.Equals(filter.AuthorFirstName));

            if (!string.IsNullOrEmpty(filter.AuthorLastName))
                books = books.Where(b => b.Author.LastName.Equals(filter.AuthorLastName));

            if (!string.IsNullOrEmpty(filter.AuthorMiddleName))
                books = books.Where(b => b.Author.MiddleName.Equals(filter.AuthorMiddleName));

            if (!string.IsNullOrEmpty(filter.GenreName))
                books = books.Where(b => b.Genres.Select(g => g.GenreName).Contains(filter.GenreName));

            return await books.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Book> GetByIdWithLibraryCardsAsync(int id)
        {
            return await _context.Book
                                 .Include(a => a.LibraryCards)
                                 .SingleOrDefaultAsync(a => a.Id == id);
        }

        /// <inheritdoc />
        public async Task<Book> GetByIdWithGenresAsync(int id)
        {
            return await _context.Book
                                 .Include(a => a.Genres)
                                 .SingleOrDefaultAsync(a => a.Id == id);
        }
    }
}
