using System;
using System.Windows.Input;

namespace PersonalJobAgent.UI.Commands
{
    using CommunityToolkit.Mvvm.Input;

    /// <summary>
    /// Implementation of the ICommand interface for WPF commands
    /// </summary>
    using System;
    using System.Windows.Input;

    namespace PersonalJobAgent.UI.Commands
    {
        public class RelayCommand<T> : ICommand
        {
            private readonly Predicate<T> _canExecute;
            private readonly Action<T> _execute;

            public RelayCommand(Action<T> execute)
                : this(execute, null)
            {
            }

            public RelayCommand(Action<T> execute, Predicate<T> canExecute)
            {
                _execute = execute ?? throw new ArgumentNullException(nameof(execute));
                _canExecute = canExecute;
            }

            public event EventHandler CanExecuteChanged;

            public bool CanExecute(object parameter)
            {
                return _canExecute == null || _canExecute((T)parameter);
            }

            public void Execute(object parameter)
            {
                _execute((T)parameter);
            }

            public void RaiseCanExecuteChanged()
            {
                CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }



    // Add this to a static class in your project
    public static class CommandExtensions
    {
        public static void RaiseCanExecuteChanged(this ICommand command)
        {
            // Just use CommandManager directly
            CommandManager.InvalidateRequerySuggested();
        }
    }



}