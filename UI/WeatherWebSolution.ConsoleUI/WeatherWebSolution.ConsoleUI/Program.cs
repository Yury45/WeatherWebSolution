using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Data.Common;
using System.Net;
using System.Threading.Tasks;
using WeatherWebSolution.DAL.Entities;
using WeatherWebSolution.Intefaces.Base.Entities.Reposytories;
using WeatherWebSolution.WebAPIClients.Repositories;

namespace WeatherWebSolution.ConsoleUI
{
    internal class Program
    {
        private static IHost __Hosting;

        public static IHost Hosting => __Hosting ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public static IServiceProvider Services => Hosting.Services;

        private static IHostBuilder CreateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)
            .ConfigureServices(ConfigureServices);

        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddHttpClient<IRepository<DataSource>, WebRepository<DataSource>>(client =>
            {
                client.BaseAddress = new Uri($"{host.Configuration["WebAPI"]}/api/DataSources/");
            });
        }


        static async Task Main(string[] args)
        {
            using var host = Hosting;
            await host.StartAsync();
            var data_sources = Services.GetRequiredService<IRepository<DataSource>>();

            var sources = await data_sources.Get(3, 5);
            foreach (var source in sources)
                Console.WriteLine($"{source.Id}: {source.Name} - {source.Description}");

            Console.WriteLine("Done");
            Console.ReadLine();
            await host.StopAsync();

        }
    }
}
