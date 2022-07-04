using System.Windows;
using WeatherWebSolution.WPF.Infrastructure.Commands.Base;

namespace WeatherWebSolution.WPF.Infrastructure.Commands
{
    internal class CloseWindow : Command
    {
        protected override void ExecuteAsync(object parameter) => (parameter as Window ?? App.FocusedWindow ?? App.ActivedWindow)?.Close();
    }
}
