using DapperDemo.WPF.State.Navigators;
using DapperDemo.WPF.ViewModels.CompanyVM;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace DapperDemo.WPF.Commands.CompanyCommands
{
    public class NavigateToDetailsCompanyCommand : ICommand
    {
        public readonly CompanyViewModel _companyViewModel;
        public readonly CompanyDetailsViewModel _companyDetailsViewModel;
        private readonly INavigator _navigator;

        public NavigateToDetailsCompanyCommand(CompanyViewModel companyViewModel, CompanyDetailsViewModel companyDetailsViewModel, INavigator navigator)
        {
            _companyViewModel = companyViewModel;
            _companyDetailsViewModel = companyDetailsViewModel;
            _navigator = navigator;

            companyViewModel.PropertyChanged += ViewModel_PropertyChanged;
            companyDetailsViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _companyViewModel.IsItemSelected;
        }

        public void Execute(object parameter)
        {
            _companyDetailsViewModel.SelectedCompanyId = _companyViewModel.SelectedCompany.CompanyId;
            _navigator.CurrentViewModel = _companyDetailsViewModel;
        }

        private void ViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
