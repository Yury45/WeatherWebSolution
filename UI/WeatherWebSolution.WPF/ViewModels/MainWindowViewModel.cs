using System.Collections.ObjectModel;
using WeatherWebSolution.DAL.Entities;
using WeatherWebSolution.Intefaces.Base.Entities.Reposytories;

namespace WeatherWebSolution.WPF.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private readonly IRepository<DataSource> _dataSources;

        public IRepository<DataSource> DataSource => _dataSources;

        public MainWindowViewModel(IRepository<DataSource> dataSources)
        {
            _dataSources = dataSources;
        }

        #region _Title : string - Название главного окна

        /// <summary>Поле под название приложения</summary>
        private string _Title = "Application";

        /// <summary>Свойство названия приложения</summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region _State : string - Статус приложения

        /// <summary>Статус приложения поле</summary>
        private string _State;

        /// <summary>Статус приложения свойство</summary>
        public string State
        {
            get => _State;
            set => Set(ref _State, value);
        }

        #endregion

        public ObservableCollection<DataSource> DataSources { get; } = new();

    }
}
