using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimbirHomeworkClean.Application.Contracts.Data;
using SimbirHomeworkClean.Application.Contracts.Repositories;
using SimbirHomeworkClean.Infrastructure.Data;
using SimbirHomeworkClean.Infrastructure.Repositories;

namespace SimbirHomeworkClean.Infrastructure
{
    /// <summary>
    /// Класс для настройки DI
    /// </summary>
    public static class Injection
    {
        public static IServiceCollection RegisterInfrastructerServices(this IServiceCollection services,
                                                                       IConfiguration configuration)
        {
            // Пункт задания: 1
            // Контексты
            services.AddDbContext<MainDbContext>(options =>
            {
                // Пункт задания: 4
                options.UseSqlServer(configuration.GetConnectionString("LocalConnection"))
                       .UseSnakeCaseNamingConvention();
            });

            services.AddScoped<IMainDbContext, MainDbContext>();

            // Репозитории
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<IPersonRepository, PersonRepository>();

            return services;
        }
    }
}
