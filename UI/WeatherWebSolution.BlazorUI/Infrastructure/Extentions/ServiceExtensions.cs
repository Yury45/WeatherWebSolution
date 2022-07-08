﻿using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace WeatherWebSolution.BlazorUI.Infrastructure.Extentions
{
    internal static class ServiceExtensions
    {
        public static IHttpClientBuilder AddApi<IInterface, IClient>(this IServiceCollection services, string address)
            where IInterface : class where IClient : class, IInterface=>
            services.AddHttpClient<IInterface, IClient>((host, client) 
                => client.BaseAddress = new(
                    $"{host.GetRequiredService<IWebAssemblyHostEnvironment>().BaseAddress}{address}"));
    }
}
