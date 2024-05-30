// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WarResolverClient.BackgroundWorkers;
using WarResolverClient.Services;
using WarResolverClient.Services.Interfaces;

public class Program
{
    public static void Main(string[] args) //@request Console app that get information from a msg broker
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<WarResolverWorker>(); //@request In background the worker is instatiated as a Singleton
                services.AddScoped<IWarResolverService, WarResolverService>(); //@request scoped service
                services.AddTransient<IBattleAgregatorService, BattleAgregatorService>(); // @request small stateless service used to perform some computation
            });
}