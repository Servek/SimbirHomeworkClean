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
        public DbSet<Author> Author { get; }

        /// <summary>
        /// Жанры
        /// </summary>
        public DbSet<Genre> Genre { get; }

        /// <summary>
        /// Люди
        /// </summary>
        public DbSet<Person> Person { get; }

        /// <summary>
        /// Книги
        /// </summary>
        public DbSet<Book> Book { get; }

        /// <summary>
        /// Записи о получении книг людьми
        /// </summary>
        public DbSet<LibraryCard> LibraryCard { get; }
    }
}
