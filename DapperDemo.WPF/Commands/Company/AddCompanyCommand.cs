using DapperDemo.WPF.State.Navigators;
using DapperDemo.WPF.ViewModels.CompanyVM;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace DapperDemo.WPF.Commands.Company
{
    public class AddCompanyCommand : ICommand
    {
        public readonly CompanyViewModel _companyViewModel;
        private readonly IRenavigator _renavigator;

        public AddCompanyCommand(CompanyViewModel companyViewModel, IRenavigator renavigator)
        {
            _companyViewModel = companyViewModel;
            _renavigator = renavigator;

            companyViewModel.PropertyChanged += CompanyViewModel_PropertyChanged;
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

        private void CompanyViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
