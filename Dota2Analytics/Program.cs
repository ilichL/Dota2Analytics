using Dota2Analytics.Data;
using Dota2Analytics.Infrastructure.Repositories.Abstractions;
using Dota2Analytics.Infrastructure.Repositories.Implementations;
using Dota2Analytics.Middleware;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Dota2Analytics
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("log.txt")
                .CreateLogger();

            var builder = WebApplication.CreateBuilder(args);
            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            // Получаем строку подключения из конфигурации
            var connectionString = builder.Configuration.GetConnectionString("Default");
            builder.Services.AddDbContext<DotaContext>(options =>
                options.UseNpgsql(connectionString));

            builder.Services.AddScoped<LoggingMiddleware>();
            builder.Services.AddScoped<IHeroRepository, HeroRepository>();
            builder.Services.AddScoped<IIteamRepository, IteamRepository>();
            builder.Services.AddScoped<IItemPurchaseRepository, ItemPurchaseRepository>();
            builder.Services.AddScoped<IMatchEventRepository, MatchEventRepository>();
            builder.Services.AddScoped<IMatchRepository, MatchRepository>();
            builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
            // Строим приложение
            var app = builder.Build();

            // Настраиваем конвейер HTTP запросов
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Применяем миграции автоматически при запуске
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<DotaContext>();
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }

            app.UseMiddleware<LoggingMiddleware>();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}