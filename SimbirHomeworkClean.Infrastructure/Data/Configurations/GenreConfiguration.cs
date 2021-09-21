using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimbirHomeworkClean.Domain.Entities;
using SimbirHomeworkClean.Infrastructure.Data.Configurations.Base;

// Пункт задания: 3
namespace SimbirHomeworkClean.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Настройка сущности жанра
    /// </summary>
    public class GenreConfiguration : AuditableEntityConfiguration<Genre>
    {
        /// <inheritdoc />
        public override void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.HasKey(g => g.Id);

            builder.Property(g => g.GenreName)
                   .IsRequired();

            // Добавил индекс, т.к. получил ответ: "Возможность добавить книгу, автор и жанр возможно присутствуют, их
            // нужно найти - если нет создать. Передавать инфомрацию о жанре и авторе, а не ID".
            // Если индекс не добавлять, тогда может получиться ситуация, когда у нас есть несколько жанров с одинаковым
            // названием и тогда не ясно, какому автору приписывать книгу, ведь ID мы не передаем.
            builder.HasIndex(g => g.GenreName)
                   .IsUnique();

            builder.HasMany(g => g.Books)
                   .WithMany(b => b.Genres);

            base.Configure(builder);
        }
    }
}
