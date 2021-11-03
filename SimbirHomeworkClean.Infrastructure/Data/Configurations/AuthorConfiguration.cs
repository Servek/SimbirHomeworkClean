using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimbirHomeworkClean.Domain.Entities;
using SimbirHomeworkClean.Infrastructure.Data.Configurations.Base;

// Пункт задания: 3
namespace SimbirHomeworkClean.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Настройка сущности автора
    /// </summary>
    public class AuthorConfiguration : AuditableEntityConfiguration<Author>
    {
        /// <inheritdoc />
        public override void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(a => a.Id);
            
            builder.Property(a => a.FirstName)
                   .IsRequired();

            builder.Property(a => a.LastName)
                   .IsRequired();

            // Добавил индекс, т.к. получил ответ: "Возможность добавить книгу, автор и жанр возможно присутствуют, их
            // нужно найти - если нет создать. Передавать инфомрацию о жанре и авторе, а не ID".
            // Если индекс не добавлять, тогда может получиться ситуация, когда у нас есть несколько авторов с
            // одинаковым ФИО ыи тогда не ясно, какому автору приписывать книгу, ведь ID мы не передаем.
            builder.HasIndex(a => new { a.FirstName, a.LastName, a.MiddleName })
                   .IsUnique();
            
            base.Configure(builder);
        }
    }
}
