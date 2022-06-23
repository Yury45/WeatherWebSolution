using System.Threading.Tasks;

namespace WeatherWebSolution.WPF.Infrastructure.Commands.Base
{
    internal delegate Task ActionAsync();

    internal delegate Task ActionAsync<in T>(T parameter);
}
