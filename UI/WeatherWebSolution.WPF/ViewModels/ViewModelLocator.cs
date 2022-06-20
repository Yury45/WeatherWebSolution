using Microsoft.Extensions.DependencyInjection;

namespace WeatherWebSolution.WPF.ViewModels
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel => App.Services.GetRequiredService<MainWindowViewModel>();
    }
}
