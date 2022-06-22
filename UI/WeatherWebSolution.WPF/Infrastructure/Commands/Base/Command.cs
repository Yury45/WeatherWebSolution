using System;
using System.Windows.Input;

namespace WeatherWebSolution.WPF.Infrastructure.Commands.Base
{
    internal abstract class Command : ICommand
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

        #region Явная реализация интерфейса ICommand
        /// <summary> Явная реализация интерфейса для доступа к командам из типа ICommand</summary>
        bool ICommand.CanExecute(object parameter) => CanExecute(parameter) && _Executable;

        void ICommand.Execute(object parameter)
        {
            if (CanExecute(parameter))
                Execute(parameter);
        }
        #endregion


        protected virtual bool CanExecute(object parameter) => true;

        protected abstract void Execute(object parameter);
    }
}
