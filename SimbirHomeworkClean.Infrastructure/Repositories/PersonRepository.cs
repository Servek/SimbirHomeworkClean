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
    /// Репозиторий людей
    /// </summary>
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="context">Контекст базы данных</param>
        public PersonRepository(MainDbContext context) : base(context) { }

        /// <inheritdoc />
        public async Task<List<Person>> GetFilteredListAsync(PersonFilter filter)
        {
            if (filter == null)
                throw new NullReferenceException();

            var people = _context.Person.AsNoTracking();

            if (!string.IsNullOrEmpty(filter.FirstName))
                people = people.Where(p => p.FirstName == filter.FirstName);

            if (!string.IsNullOrEmpty(filter.LastName))
                people = people.Where(p => p.LastName == filter.LastName);

            if (!string.IsNullOrEmpty(filter.MiddleName))
                people = people.Where(p => p.MiddleName == filter.MiddleName);

            return await people.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<List<LibraryCard>> GetLibraryCardsAsync(int id)
        {
            return await _context.LibraryCard
                                 .Where(lc => lc.PersonId == id)
                                 .Include(lc => lc.Book)
                                 .ThenInclude(b => b.Author)
                                 .Include(lc => lc.Book)
                                 .ThenInclude(b => b.Genres)
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        /// <inheritdoc />
        public async Task<Person> ReceiveBookAsync(Person entity, int bookId)
        {
            await _context.LibraryCard.AddAsync(new LibraryCard
            {
                PersonId = entity.Id,
                BookId = bookId,
                ObtainedDateTime = DateTimeOffset.Now
            });

            await _context.SaveChangesAsync();

            return await _context.Person
                                 .Where(p => p.Id == entity.Id)
                                 .Include(lc => lc.LibraryCards)
                                 .ThenInclude(b => b.Book)
                                 .AsNoTracking()
                                 .SingleOrDefaultAsync();
        }

        /// <inheritdoc />
        public async Task<Person> ReturnBookAsync(Person entity, int bookId)
        {
            var libraryCard = await _context.LibraryCard.SingleOrDefaultAsync(lc => lc.PersonId == entity.Id && lc.BookId == bookId);
            _context.Remove(libraryCard);
            await _context.SaveChangesAsync();

            return await _context.Person
                                 .Where(p => p.Id == entity.Id)
                                 .Include(lc => lc.LibraryCards)
                                 .ThenInclude(b => b.Book)
                                 .AsNoTracking()
                                 .SingleOrDefaultAsync();
        }
    }
}
