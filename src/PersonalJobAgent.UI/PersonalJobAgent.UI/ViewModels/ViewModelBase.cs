using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using PersonalJobAgent.UI.Commands;

namespace PersonalJobAgent.UI.ViewModels
{
    /// <summary>
    /// Base class for all view models
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event
        /// </summary>
        /// <param name="propertyName">Name of the property that changed</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Sets a property value and raises the PropertyChanged event if the value has changed
        /// </summary>
        /// <typeparam name="T">Type of the property</typeparam>
        /// <param name="storage">Reference to the backing field</param>
        /// <param name="value">New value</param>
        /// <param name="propertyName">Name of the property</param>
        /// <returns>True if the value was changed, false otherwise</returns>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Creates a RelayCommand with the specified execute action
        /// </summary>
        /// <param name="execute">Action to execute</param>
        /// <returns>RelayCommand instance</returns>
        protected RelayCommand CreateCommand(Action execute)
        {
            return new RelayCommand(execute);
        }

        /// <summary>
        /// Creates a RelayCommand with the specified execute action and can execute function
        /// </summary>
        /// <param name="execute">Action to execute</param>
        /// <param name="canExecute">Function that determines if the command can execute</param>
        /// <returns>RelayCommand instance</returns>
        protected RelayCommand CreateCommand(Action execute, Func<bool> canExecute)
        {
            return new RelayCommand(execute, canExecute);
        }
    }
}
