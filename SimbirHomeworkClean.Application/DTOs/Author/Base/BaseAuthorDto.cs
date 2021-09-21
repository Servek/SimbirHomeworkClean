using System.ComponentModel.DataAnnotations;

namespace SimbirHomeworkClean.Application.DTOs.Author.Base
{
    /// <summary>
    /// Базовый транспортный объект автора без идентификатора и внешних сущностей
    /// </summary>
    public abstract class BaseAuthorDto
    {
        /// <summary>
        /// Имя
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; }
    }
}
