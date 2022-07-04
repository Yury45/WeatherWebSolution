using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherWebSolution.DAL.Entities;
using WeatherWebSolution.WPF.Infrastructure.Commands.Base;
using WeatherWebSolution.WPF.ViewModels;

namespace WeatherWebSolution.WPF.Infrastructure.Commands
{
    internal class LoadDataSources : Command
    {
        protected override async void ExecuteAsync(object p)
        {
            var mainWindowViewModel = p as MainWindowViewModel;

            mainWindowViewModel.DataSources.Clear();

            var sources = await mainWindowViewModel.DataSource.GetAll();

            foreach (var source in sources)
            {
                mainWindowViewModel.DataSources.Add(source);
            }
        }
    }
}
