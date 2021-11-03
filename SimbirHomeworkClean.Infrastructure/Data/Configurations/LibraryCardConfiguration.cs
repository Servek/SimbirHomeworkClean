using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimbirHomeworkClean.Domain.Entities;

// Пункт задания: 3
namespace SimbirHomeworkClean.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Настройка сущности записи о получении книги человеком
    /// </summary>
    public class LibraryCardConfiguration : IEntityTypeConfiguration<LibraryCard>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<LibraryCard> builder)
        {
            builder.HasKey(lc => new { lc.PersonId, lc.BookId });

            builder.HasOne(lc => lc.Person)
                   .WithMany(p => p.LibraryCards)
                   .HasForeignKey(lc => lc.PersonId);

            builder.HasOne(lc => lc.Book)
                   .WithMany(b => b.LibraryCards)
                   .HasForeignKey(lc => lc.BookId);
        }
    }
}
