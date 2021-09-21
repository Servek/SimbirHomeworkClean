namespace SimbirHomeworkClean.Application.DTOs.Author
{
    /// <summary>
    /// Запрос на список книг
    /// </summary>
    public class AuthorsQuery
    {
        /// <summary>
        /// Год написания книги
        /// </summary>
        public int? BookWritingYear { get; set; }

        /// <summary>
        /// Подстрока названия книги
        /// </summary>
        public string BookNameTerm { get; set; }

        /// <summary>
        /// Является ли сортировка по убыванию
        /// </summary>
        public bool IsDescOrder { get; set; }
    }
}
