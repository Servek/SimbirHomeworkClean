using System.ComponentModel.DataAnnotations;
using SimbirHomeworkClean.Application.DTOs.Author.Base;

namespace SimbirHomeworkClean.Application.DTOs.Author
{
    /// <summary>
    /// Транспортный объект автора
    /// </summary>
    public class AuthorDto : BaseAuthorDto
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Required]
        public int Id { get; set; }
    }
}
