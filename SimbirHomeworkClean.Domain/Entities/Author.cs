using System.Collections.Generic;
using SimbirHomeworkClean.Domain.Entities.Base;

// Пункт задания: 2
namespace SimbirHomeworkClean.Domain.Entities
{
    /// <summary>
    /// Модель автора
    /// </summary>
    public class Author : AuditableEntity
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }
        
        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Книги автора
        /// </summary>
        public ICollection<Book> Books { get; set; } = new HashSet<Book>();
    }
}
