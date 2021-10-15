using System;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SimbirHomeworkClean.Domain.Entities;
using SimbirHomeworkClean.Infrastructure.Data;

namespace SimbirHomeworkClean.Infrastructure.UnitTests.Fixtures
{
    /// <summary>
    /// Fixture базы данных
    /// </summary>
    public class DatabaseFixture : IDisposable
    {
        /// <summary>
        /// Настройки для контекста базы данных
        /// </summary>
        private readonly DbContextOptions<MainDbContext> _contextOptions;

        /// <summary>
        /// Подключение к базе данных
        /// </summary>
        public DbConnection Connection { get; }

        // Пункт задания: 2.1.
        /// <summary>
        /// Конструктор
        /// </summary>
        public DatabaseFixture()
        {
            Connection = new SqliteConnection("Filename=:memory:");
            Connection.Open();
            _contextOptions = new DbContextOptionsBuilder<MainDbContext>()
                             .UseSqlite(Connection)
                             .Options;

            Seed();
        }

        /// <summary>
        /// Создать контекст базы данных
        /// </summary>
        /// <param name="transaction">Транзакция базы данных</param>
        /// <returns></returns>
        public MainDbContext CreateContext(DbTransaction transaction = null)
        {
            var context = new MainDbContext(_contextOptions);

            if (transaction != null)
                context.Database.UseTransaction(transaction);

            return context;
        }

        // Пункт задания: 2.2.
        /// <summary>
        /// Инициализация базы данных стартовыми значениями
        /// </summary>
        private void Seed()
        {
            using var context = CreateContext();

            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            #region Жанры
            var genre1 = new Genre { Id = 1, GenreName = "Роман" };
            var genre2 = new Genre { Id = 2, GenreName = "Фантастика" };
            var genre3 = new Genre { Id = 3, GenreName = "Детектив" };

            context.AddRange(genre1, genre2, genre3);
            #endregion

            #region Авторы
            var author1 = new Author { Id = 1, FirstName = "Лев", LastName = "Толстой", MiddleName = "Николаевич" };
            var author2 = new Author { Id = 2, FirstName = "Гарри", LastName = "Гаррисон" };
            var author3 = new Author { Id = 3, FirstName = "Агата", LastName = "Кристи" };

            context.AddRange(author1, author2, author3);
            #endregion

            #region Книги
            var book1 = new Book { Id = 1, Name = "Война и мир", Author = author1, WritingYear = 1869, Genres = { genre1 } };
            var book2 = new Book { Id = 2, Name = "Рождение Стальной Крысы", Author = author2, WritingYear = 1985, Genres = { genre2, genre3 } };
            var book3 = new Book { Id = 3, Name = "Стальная Крыса идёт в армию", Author = author2, WritingYear = 1987, Genres = { genre2, genre3 } };
            var book4 = new Book { Id = 4, Name = "Убийство в «Восточном экспрессе»", Author = author3, WritingYear = 1933, Genres = { genre3 } };

            context.AddRange(book1, book2, book3, book4);
            #endregion

            #region Люди
            var person1 = new Person { Id = 1, FirstName = "Александр", LastName = "Арсирий", MiddleName = "Русланович" };
            var person2 = new Person { Id = 2, FirstName = "Пётр", LastName = "Петров", MiddleName = "Петрович" };

            context.AddRange(person1, person2);
            #endregion

            #region Записи о получении книг
            var libraryCard1 = new LibraryCard { Person = person1, Book = book2, ObtainedDateTime = DateTimeOffset.Parse("2021-08-09 12:00:00+03:00") };
            var libraryCard2 = new LibraryCard { Person = person1, Book = book3, ObtainedDateTime = DateTimeOffset.Parse("2021-01-01 14:00:00+03:00") };
            var libraryCard3 = new LibraryCard { Person = person2, Book = book1, ObtainedDateTime = DateTimeOffset.Parse("2021-03-01 13:00:00+03:00") };

            context.AddRange(libraryCard1, libraryCard2, libraryCard3);
            #endregion

            context.SaveChanges();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
