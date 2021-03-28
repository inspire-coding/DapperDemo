using DapperDemo.WPF.State.Navigators;
using DapperDemo.WPF.ViewModels;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace DapperDemo.WPF.Commands.CompanyCommands
{
    public class BackToEmployeeListCommand : ICommand
    {
        public readonly ViewModelBase _viewModelBase;
        private readonly IRenavigator _renavigator;

        public BackToEmployeeListCommand(ViewModelBase viewModelBase, IRenavigator renavigator)
        {
            _viewModelBase = viewModelBase;
            _renavigator = renavigator;

            _viewModelBase.PropertyChanged += CreateCompanyViewModel_PropertyChanged;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _renavigator.Renavigate();
        }

        private void CreateCompanyViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
