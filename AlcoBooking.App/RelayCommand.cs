﻿using System.Windows.Input;

namespace AlcoBooking.App
{
    internal class RelayCommand: ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Predicate<object?> _canExecute;

        public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute ?? (_ => true);
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
