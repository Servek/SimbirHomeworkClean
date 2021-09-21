using System;

// Пункт задания: 2
namespace SimbirHomeworkClean.Domain.Entities
{
    /// <summary>
    /// Модель записи о получении книги человеком
    /// </summary>
    public class LibraryCard
    {
        /// <summary>
        /// Идентификатор человека
        /// </summary>
        public int PersonId { get; set; }

        /// <summary>
        /// Человек
        /// </summary>
        public Person Person { get; set; }

        /// <summary>
        /// Идентификатор книги
        /// </summary>
        public int BookId { get; set; }

        /// <summary>
        /// Книга
        /// </summary>
        public Book Book { get; set; }

        /// <summary>
        /// Дата и время получение книги
        /// </summary>
        public DateTimeOffset ObtainedDateTime { get; set; }
    }
}
