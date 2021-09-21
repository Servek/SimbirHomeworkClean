namespace SimbirHomeworkClean.Application.Filters
{
    /// <summary>
    /// Фильтр людей
    /// </summary>
    public class BookFilter
    {
        /// <summary>
        /// Имя автора
        /// </summary>
        public string AuthorFirstName { get; set; }

        /// <summary>
        /// Фамилия автора
        /// </summary>
        public string AuthorLastName { get; set; }

        /// <summary>
        /// Отчество автора
        /// </summary>
        public string AuthorMiddleName { get; set; }

        /// <summary>
        /// Название жанра
        /// </summary>
        public string GenreName { get; set; }
    }
}
