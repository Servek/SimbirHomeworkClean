using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimbirHomeworkClean.Domain.Entities;
using SimbirHomeworkClean.Infrastructure.Data.Configurations.Base;

// Пункт задания: 3
namespace SimbirHomeworkClean.Infrastructure.Data.Configurations
{
    /// <summary>
    /// Настройка сущности книги
    /// </summary>
    public class BookConfiguration : AuditableEntityConfiguration<Book>
    {
        /// <inheritdoc />
        public override void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);
            
            builder.Property(b => b.Name)
                   .IsRequired();

            builder.Property(b => b.WritingYear)
                   .IsRequired();

            builder.HasOne(b => b.Author)
                   .WithMany(a => a.Books)
                   .HasForeignKey(b => b.AuthorId);

            builder.HasMany(b => b.Genres)
                   .WithMany(g => g.Books);
            
            base.Configure(builder);
        }
    }
}
