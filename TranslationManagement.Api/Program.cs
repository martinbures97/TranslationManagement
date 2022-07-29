using External.ThirdParty.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TranslationManagement.Api.Common;
using TranslationManagement.Application;
using TranslationManagement.Infrastructure;

namespace TranslationManagement.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();

            builder.Services.AddControllers(options =>
            {
                options.Filters.Add<ApiExceptionFilterAttribute>();
            });
            
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TranslationManagement.Api", Version = "v1" });
            });

            builder.Services.AddAutoMapper(options =>
                options.AddProfile(new MappingProfile())
            );
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure();
            builder.Services.AddTransient<INotificationService, UnreliableNotificationService>();

            var app = builder.Build();

            app.UseCors(b => b
                .WithOrigins("http://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            using (var scope = app.Services.CreateScope())
            using (var context = scope.ServiceProvider.GetRequiredService<AppDbContext>())
            {
                if (app.Environment.IsDevelopment())
                {
                    context.Database.EnsureDeleted();
                }

                context.Database.Migrate();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", 
                "TranslationManagement.Api v1"));
            app.UseRouting();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.Run();
        }
    }
}