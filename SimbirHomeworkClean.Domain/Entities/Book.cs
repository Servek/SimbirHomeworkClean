using System.Collections.Generic;
using SimbirHomeworkClean.Domain.Entities.Base;

// Пункт задания: 2
namespace SimbirHomeworkClean.Domain.Entities
{
    /// <summary>
    /// Модель книги
    /// </summary>
    public class Book : AuditableEntity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        // Пункт задания: 8.1.
        /// <summary>
        /// Год написания
        /// </summary>
        public int WritingYear { get; set; }

        /// <summary>
        /// Идентификатор автора
        /// </summary>
        public int AuthorId { get; set; }

        /// <summary>
        /// Автор
        /// </summary>
        public Author Author { get; set; }

        /// <summary>
        /// Жанры
        /// </summary>
        public ICollection<Genre> Genres { get; set; } = new HashSet<Genre>();

        /// <summary>
        /// Записи о получении книги людьми
        /// </summary>
        public ICollection<LibraryCard> LibraryCards { get; set; } = new HashSet<LibraryCard>();
    }
}
