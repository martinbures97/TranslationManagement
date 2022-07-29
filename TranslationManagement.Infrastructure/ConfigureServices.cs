namespace TranslationManagement.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options => 
            options.UseSqlite("Data Source=TranslationAppDatabase.db"));


        services.AddScoped<IApDbContext, AppDbContext>();
        
        return services;
    }
}