using DapperDemo.WPF.State.Navigators;
using DapperDemo.WPF.ViewModels.CompanyVM;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace DapperDemo.WPF.Commands.Company
{
    public class NavigateToUpsertCompanyCommand : ICommand
    {
        public readonly CompanyViewModel _companyViewModel;
        public readonly UpsertCompanyViewModel _upsertCompanyViewModel;
        private readonly INavigator _navigator;

        public NavigateToUpsertCompanyCommand(CompanyViewModel companyViewModel, UpsertCompanyViewModel upsertCompanyViewModel, INavigator navigator)
        {
            _companyViewModel = companyViewModel;
            _upsertCompanyViewModel = upsertCompanyViewModel;
            _navigator = navigator;

            companyViewModel.PropertyChanged += CompanyViewModel_PropertyChanged;
            upsertCompanyViewModel.PropertyChanged += CompanyViewModel_PropertyChanged;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _upsertCompanyViewModel.SelectedCompany = _companyViewModel.SelectedCompany;
            _navigator.CurrentViewModel = _upsertCompanyViewModel;
        }

        private void CompanyViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
