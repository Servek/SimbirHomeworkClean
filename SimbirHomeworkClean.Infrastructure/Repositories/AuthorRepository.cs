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
    /// Репозиторий авторов
    /// </summary>
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public AuthorRepository(MainDbContext context) : base(context) { }

        /// <inheritdoc />
        public async Task<List<Author>> GetFilteredListAsync(AuthorFilter filter, bool isDescOrder)
        {
            if (filter == null)
                throw new NullReferenceException();

            var authors = _context.Author.AsNoTracking();

            if (!string.IsNullOrEmpty(filter.FirstName))
                authors = authors.Where(a => a.FirstName == filter.FirstName);

            if (!string.IsNullOrEmpty(filter.LastName))
                authors = authors.Where(a => a.LastName == filter.LastName);

            if (!string.IsNullOrEmpty(filter.MiddleName))
                authors = authors.Where(a => a.MiddleName == filter.MiddleName);

            if (filter.FullName.HasValue)
            {
                authors = authors.Where(a => a.FirstName == filter.FullName.Value.FirstName
                                          && a.LastName == filter.FullName.Value.LastName
                                          && a.MiddleName == filter.FullName.Value.MiddleName);
            }

            if (filter.BookWritingYear.HasValue)
                authors = authors.Where(a => a.Books.Select(b => b.WritingYear).Contains(filter.BookWritingYear.Value));

            if (!string.IsNullOrEmpty(filter.BookNameTerm))
                authors = authors.Where(a => a.Books.Any(b => EF.Functions.Like(b.Name, $"%{filter.BookNameTerm}%")));

            if (isDescOrder)
            {
                authors = authors.OrderByDescending(a => a.LastName)
                                 .ThenByDescending(a => a.FirstName);
            }
            else
            {
                authors = authors.OrderBy(a => a.LastName)
                                 .ThenBy(a => a.FirstName);
            }

            return await authors.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Author> GetByIdWithBooksAsync(int id)
        {
            return await _context.Author
                                 .Include(a => a.Books)
                                 .ThenInclude(b => b.Genres)
                                 .SingleOrDefaultAsync(a => a.Id == id);
        }
    }
}
