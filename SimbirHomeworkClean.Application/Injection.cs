﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimbirHomeworkClean.Application.Contracts.Services;
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
            // Лекции 4-5. Пункт задания: 2 (уже было до этого задания)
            // Маппер
            services.AddAutoMapper(typeof(Injection));

            // Сервисы
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IPersonService, PersonService>();

            return services;
        }
    }
}
