using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimbirHomeworkClean.Application.Contracts.Data;
using SimbirHomeworkClean.Domain.Entities;
using SimbirHomeworkClean.Domain.Entities.Base;

namespace SimbirHomeworkClean.Infrastructure.Data
{
    /// <summary>
    /// Основной контекст базы данных
    /// </summary>
    public class MainDbContext : DbContext, IMainDbContext
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="options">Настройки контекста</param>
        public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

        /// <inheritdoc />
        public DbSet<Author> Author { get; set; }

        /// <inheritdoc />
        public DbSet<Genre> Genre { get; set; }

        /// <inheritdoc />
        public DbSet<Person> Person { get; set; }

        /// <inheritdoc />
        public DbSet<Book> Book { get; set; }

        /// <inheritdoc />
        public DbSet<LibraryCard> LibraryCard { get; set; }

        /// <inheritdoc />
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        // Пункт задания: 9.1.1.
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.Updated = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        // Пункт задания: 9.1.2.
                        entry.Entity.Updated = DateTime.Now;
                        break;
                }
            }
            
            return await base.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
