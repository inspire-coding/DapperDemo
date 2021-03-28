using DapperDemo.WPF.State.Navigators;
using DapperDemo.WPF.ViewModels.EmployeeVM;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace DapperDemo.WPF.Commands.CompanyCommands
{
    public class NavigateToAddEmployeeCommand : ICommand
    {
        public readonly EmployeesViewModel _employeesViewModel;
        public readonly UpsertEmployeeViewModel _upsertEmployeeViewModel;
        private readonly INavigator _navigator;

        public NavigateToAddEmployeeCommand(EmployeesViewModel employeesViewModel, UpsertEmployeeViewModel upsertEmployeeViewModel, INavigator navigator)
        {
            _employeesViewModel = employeesViewModel;
            _upsertEmployeeViewModel = upsertEmployeeViewModel;
            _navigator = navigator;

            employeesViewModel.PropertyChanged += CompanyViewModel_PropertyChanged;
            upsertEmployeeViewModel.PropertyChanged += CompanyViewModel_PropertyChanged;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _navigator.CurrentViewModel = _upsertEmployeeViewModel;
        }

        private void CompanyViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
