using Microsoft.EntityFrameworkCore;
using SimbirHomeworkClean.Domain.Entities;

namespace SimbirHomeworkClean.Application.Contracts.Data
{
    /// <summary>
    /// Интерфейс основного контекста базы данных
    /// </summary>
    public interface IMainDbContext
    {
        /// <summary>
        /// Авторы
        /// </summary>
        DbSet<Author> Author { get; }

        /// <summary>
        /// Жанры
        /// </summary>
        DbSet<Genre> Genre { get; }

        /// <summary>
        /// Люди
        /// </summary>
        DbSet<Person> Person { get; }

        /// <summary>
        /// Книги
        /// </summary>
        DbSet<Book> Book { get; }

        /// <summary>
        /// Записи о получении книг людьми
        /// </summary>
        DbSet<LibraryCard> LibraryCard { get; }
    }
}
