using Microsoft.EntityFrameworkCore;
using NinjaWorld.Application.Interfaces;
using NinjaWorld.Application.Services;
using NinjaWorld.Infrastructure.Data;
using Npgsql;

namespace NinjaWorld.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, string connectionString)
        {
            Console.WriteLine("Connecting to db with ConnectionString: " + connectionString);
            var npgsqlDataSource = new NpgsqlDataSourceBuilder(connectionString);
            var npgsqlDataSourceBuilder = npgsqlDataSource.Build();

            services.AddDbContext<INinjaDbContext, NinjaDbContext>((sp, options) =>
            {
                options.UseNpgsql(npgsqlDataSourceBuilder)
                    .UseSnakeCaseNamingConvention();
            });
            return services;
        }
    }
}
