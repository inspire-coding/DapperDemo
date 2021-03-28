using DapperDemo.WPF.ViewModels;
using DapperDemo.WPF.ViewModels.CompanyVM;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace DapperDemo.WPF.Utils.DialogHelper
{
    public class ActionCommand : ICommand
    {
        public readonly ViewModelBase _viewModelBase;
        private readonly Action<object> action;
        private readonly Predicate<Object> predicate;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionCommand"/> class.
        /// </summary>
        /// <param name="action">The action to invoke on command.</param>
        public ActionCommand(ViewModelBase viewModelBase, Action<Object> action) : this(viewModelBase, action, null)
        {
            _viewModelBase = viewModelBase;
            _viewModelBase.PropertyChanged += ViewModelBase_PropertyChanged;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionCommand"/> class.
        /// </summary>
        /// <param name="action">The action to invoke on command.</param>
        /// <param name="predicate">The predicate that determines if the action can be invoked.</param>
        public ActionCommand(ViewModelBase viewModelBase, Action<Object> action, Predicate<Object> predicate)
        {
            _viewModelBase = viewModelBase;
            _viewModelBase.PropertyChanged += ViewModelBase_PropertyChanged;

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action), @"You must specify an Action<T>.");
            }

            this.action = action;
            this.predicate = predicate;
        }

        /// <summary>
        /// Occurs when the <see cref="System.Windows.Input.CommandManager"/> detects conditions that might change the ability of a command to execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Determines whether the command can execute.
        /// </summary>
        /// <param name="parameter">A custom parameter object.</param>
        /// <returns>
        ///     Returns true if the command can execute, otherwise returns false.
        /// </returns>
        public bool CanExecute(object parameter)
        {
            if (this.predicate == null)
            {
                return true;
            }
            return this.predicate(parameter);
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        public void Execute()
        {
            Execute(null);
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="parameter">A custom parameter object.</param>
        public void Execute(object parameter)
        {
            this.action(parameter);
        }

        private void ViewModelBase_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
