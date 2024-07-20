using Microsoft.EntityFrameworkCore;

namespace NaturalGasApp.EfStructures;

public static class DbExtensions
{
    public static IServiceCollection AddNaturalGasDbContext(this IServiceCollection services, string databaseName)
    {
        var connectionString =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), databaseName);

        services.AddDbContext<NaturalGasDbContext>(options =>
            options.UseSqlite($"Data Source={connectionString}"));
        return services;
    }
}