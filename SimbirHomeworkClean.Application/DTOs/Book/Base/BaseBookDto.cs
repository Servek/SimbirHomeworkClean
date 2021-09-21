using System.ComponentModel.DataAnnotations;

namespace SimbirHomeworkClean.Application.DTOs.Book.Base
{
    /// <summary>
    /// Базовый транспортный объект книги без идентификатора и внешних сущностей
    /// </summary>
    public abstract class BaseBookDto
    {
        /// <summary>
        /// Название
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Год написания
        /// </summary>
        [Required]
        public int WritingYear { get; set; }
        
    }
}
