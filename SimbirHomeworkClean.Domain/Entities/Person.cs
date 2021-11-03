using System;
using System.Collections.Generic;
using SimbirHomeworkClean.Domain.Entities.Base;

// Пункт задания: 2
namespace SimbirHomeworkClean.Domain.Entities
{
    /// <summary>
    /// Модель человека
    /// </summary>
    public class Person : AuditableEntity
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
        /// Дата рождения
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Записи о получении книг
        /// </summary>
        public ICollection<LibraryCard> LibraryCards { get; set; } = new HashSet<LibraryCard>();
    }
}
