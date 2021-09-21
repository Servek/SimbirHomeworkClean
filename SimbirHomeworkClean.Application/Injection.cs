using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimbirHomeworkClean.Application.Contracts.Services;
using SimbirHomeworkClean.Application.Mapping;
using SimbirHomeworkClean.Application.Services;

namespace SimbirHomeworkClean.Application
{
    public static class Injection
    {
        /// <summary>
        /// Класс для настройки DI
        /// </summary>
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services,
                                                                     IConfiguration configuration)
        {
            // Маппер
            services.AddAutoMapper(typeof(AuthorMappingProfile),
                                   typeof(BookMappingProfile),
                                   typeof(GenreMappingProfile),
                                   typeof(PersonMappingProfile),
                                   typeof(LibraryCardMappingProfile));
            
            // Сервисы
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IPersonService, PersonService>();
            
            return services;
        }
    }
}
