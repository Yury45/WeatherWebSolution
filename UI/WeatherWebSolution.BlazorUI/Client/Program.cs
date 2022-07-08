using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WeatherWebSolution.BlazorUI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WeatherWebSolution.BlazorUI.Infrastructure.Extentions;
using WeatherWebSolution.BlazorUI.Pages;
using WeatherWebSolution.Domain.Base;
using WeatherWebSolution.Intefaces.Base.Entities.Reposytories;
using WeatherWebSolution.WebAPIClients.Repositories;

namespace WeatherWebSolution.BlazorUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            var services = builder.Services;

            services.AddScoped(sp => new HttpClient
                { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            services.AddApi<IRepository<DataSourceInfo>, WebRepository<DataSourceInfo>>("api/SourcesRepository");

            await builder.Build().RunAsync();
        }
    }
}