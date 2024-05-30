using NinjaWorld.Application.BackgroundWorkers;
using NinjaWorld.Application.Interfaces;
using NinjaWorld.Application.Services;

namespace NinjaWorld.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            services.AddScoped<INinjaService, NinjaService>();
            services.AddHostedService<WarAftermatchWorker>();
            return services;
        }
    }
}