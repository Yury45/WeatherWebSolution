using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WeatherWebSolution.WPF.Infrastructure.Commands.Base
{
    internal abstract class CommandAsync : ICommand
    {
        #region _Executable : bool - Механизм валидации применения команды

        /// <summary>Механизм валидации применения команды поле</summary>
        private bool _Executable = true;

        public bool Executable
        {
            get => _Executable;
            set
            {
                if (_Executable == value) return;
                _Executable = value;
                CommandManager.InvalidateRequerySuggested();
            }
        }

        #endregion

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        #region Async явная реализация интерфейса ICommand

        /// <summary> Явная реализация интерфейса для доступа к командам из типа ICommand</summary>
        bool ICommand.CanExecute(object parameter) => CanExecute(parameter) && _Executable;

        async void ICommand.Execute(object parameter)
        {
            if (!((ICommand)this).CanExecute(parameter)) return;
            try
            {
                Executable = false;
                await ExecuteAsync(parameter);
            }
            catch
            {
                Executable = true;
                throw;
            }
        }

        #endregion

        protected virtual bool CanExecute(object parameter) => true;

        protected abstract Task ExecuteAsync(object parameter);
    }
}
