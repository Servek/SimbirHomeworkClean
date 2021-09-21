using SimbirHomeworkClean.Domain.Structs;

namespace SimbirHomeworkClean.Application.Filters
{
    /// <summary>
    /// Фильтр авторов
    /// </summary>
    public class AuthorFilter
    {
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
        /// ФИО
        /// </summary>
        public FullName? FullName { get; set; }

        /// <summary>
        /// Год написания книги
        /// </summary>
        public int? BookWritingYear { get; set; }

        /// <summary>
        /// Подстрока названия книги
        /// </summary>
        public string BookNameTerm { get; set; }
    }
}
