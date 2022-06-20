using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WeatherWebSolution.WPF.ViewModels;

namespace WeatherWebSolution.WPF.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {

        #region _Text : string - Текстовое значение

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




    }
}
