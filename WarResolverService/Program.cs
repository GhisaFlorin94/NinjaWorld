// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WarResolverClient.BackgroundWorkers;
using WarResolverClient.Services;
using WarResolverClient.Services.Interfaces;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<WarResolverWorker>(); // In background the worker is instatiated as a Singleton
                services.AddScoped<IWarResolverService, WarResolverService>();
                services.AddTransient<IBattleAgregatorService, BattleAgregatorService>(); // small statelles service used to perform some computation
            });
}