using System;
using System.IO;
using System.Reflection;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SimbirHomeworkClean.Application;
using SimbirHomeworkClean.Application.DTOs.Author;
using SimbirHomeworkClean.Infrastructure;

namespace SimbirHomeworkClean.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                    .AddFluentValidation(fv =>
                     {
                         fv.RegisterValidatorsFromAssemblyContaining<AuthorDto>(lifetime: ServiceLifetime.Singleton);
                     });

            // Лекции 4-5. Пункт задания: 3
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SimbirHomeworkClean.Api", Version = "v1" });

                var filePathApi = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
                var filePathApp = Path.Combine(AppContext.BaseDirectory, $"{typeof(AuthorDto).Assembly.GetName().Name}.xml");
                c.IncludeXmlComments(filePathApi);
                c.IncludeXmlComments(filePathApp);
            });

            services.AddFluentValidationRulesToSwagger();

            services.RegisterApplicationServices(Configuration);
            services.RegisterInfrastructerServices(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Лекции 4-5. Пункт задания: 3 (чтобы с релизной версии открыть сваггер вынес его из "Development")
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SimbirHomeworkClean.Api v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
