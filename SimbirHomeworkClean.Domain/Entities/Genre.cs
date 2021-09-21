using System.Collections.Generic;
using SimbirHomeworkClean.Domain.Entities.Base;

// Пункт задания: 2
namespace SimbirHomeworkClean.Domain.Entities
{
    /// <summary>
    /// Модель жанра
    /// </summary>
    public class Genre : AuditableEntity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string GenreName { get; set; }

        /// <summary>
        /// Книги данного жанра
        /// </summary>
        public ICollection<Book> Books { get; set; } = new HashSet<Book>();
    }
}
