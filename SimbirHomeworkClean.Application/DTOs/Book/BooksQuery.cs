namespace SimbirHomeworkClean.Application.DTOs.Book
{
    /// <summary>
    /// Запрос на список книг
    /// </summary>
    public class BooksQuery
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
