using DapperDemo.Data.Repository;
using DapperDemo.WPF.State.Navigators;
using DapperDemo.WPF.ViewModels.CompanyVM;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace DapperDemo.WPF.Commands.CompanyCommands
{
    public class AddCompanyCommand : ICommand
    {
        private readonly ICompanyRepository _compRepo;
        public readonly UpsertCompanyViewModel _addCompanyViewModel;
        private readonly IRenavigator _renavigator;

        public AddCompanyCommand(UpsertCompanyViewModel addCompanyViewModel, ICompanyRepository compRepo, IRenavigator renavigator)
        {
            _compRepo = compRepo;
            _addCompanyViewModel = addCompanyViewModel;
            _renavigator = renavigator;

            _addCompanyViewModel.PropertyChanged += CreateCompanyViewModel_PropertyChanged;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _addCompanyViewModel.CanAddCompany;
        }

        public async void Execute(object parameter)
        {
            await _compRepo.Add(_addCompanyViewModel.Company);

            _renavigator.Renavigate();
        }

        private void CreateCompanyViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
