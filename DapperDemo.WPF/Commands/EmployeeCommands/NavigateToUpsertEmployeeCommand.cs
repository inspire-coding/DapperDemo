using DapperDemo.WPF.State.Navigators;
using DapperDemo.WPF.ViewModels.EmployeeVM;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace DapperDemo.WPF.Commands.Company
{
    public class NavigateToUpsertEmployeeCommand : ICommand
    {
        public readonly EmployeesViewModel _employeesViewModel;
        public readonly UpsertEmployeeViewModel _upsertEmployeeViewModel;
        private readonly INavigator _navigator;

        public NavigateToUpsertEmployeeCommand(EmployeesViewModel employeesViewModel, UpsertEmployeeViewModel upsertEmployeeViewModel, INavigator navigator)
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
            return _employeesViewModel.IsItemSelected;
        }

        public void Execute(object parameter)
        {
            _upsertEmployeeViewModel.SelectedEmployee = _employeesViewModel.SelectedEmployee;
            _navigator.CurrentViewModel = _upsertEmployeeViewModel;
        }

        private void CompanyViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
