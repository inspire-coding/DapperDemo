using DapperDemo.Data.Repository;
using DapperDemo.WPF.State.Navigators;
using DapperDemo.WPF.Utils;
using DapperDemo.WPF.ViewModels.CompanyVM;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace DapperDemo.WPF.Commands.CompanyCommands
{
    public class UpsertCompanyCommand : ICommand
    {
        private readonly ICompanyRepository _compRepo;
        public readonly UpsertCompanyViewModel _addCompanyViewModel;
        private readonly IRenavigator _renavigator;
        private readonly UpsertAction _upsertAction;

        public UpsertCompanyCommand(UpsertCompanyViewModel addCompanyViewModel, ICompanyRepository compRepo, UpsertAction upsertAction, IRenavigator renavigator)
        {
            _compRepo = compRepo;
            _addCompanyViewModel = addCompanyViewModel;
            _renavigator = renavigator;
            _upsertAction = upsertAction;

            _addCompanyViewModel.PropertyChanged += CreateCompanyViewModel_PropertyChanged;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _addCompanyViewModel.CanAddCompany;
        }

        public async void Execute(object parameter)
        {
            switch (_upsertAction)
            {
                case UpsertAction.Add:
                    await _compRepo.Add(_addCompanyViewModel.Company);
                    break;
                case UpsertAction.Update:
                    await _compRepo.Update(_addCompanyViewModel.Company);
                    break;
                default:
                    break;
            }

            _renavigator.Renavigate();
        }

        private void CreateCompanyViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
