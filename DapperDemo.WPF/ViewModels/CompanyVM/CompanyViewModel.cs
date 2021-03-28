using DapperDemo.Data.Models;
using DapperDemo.Data.Repository;
using DapperDemo.WPF.Commands;
using DapperDemo.WPF.Commands.Company;
using DapperDemo.WPF.Commands.CompanyCommands;
using DapperDemo.WPF.State.Navigators;
using DapperDemo.WPF.Utils.DialogHelper;
using DapperDemo.WPF.ViewModels.Dialog;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DapperDemo.WPF.ViewModels.CompanyVM
{
    public class CompanyViewModel : ViewModelBase
    {
        private readonly ICompanyRepository _compRepo;
        private readonly IDialogService _dialogService;



        private readonly ObservableCollection<Company> _companies;
        public IEnumerable<Company> Companies => _companies;


        /*
         * Properties
         */
        private Company _selectedCompany;

        public Company SelectedCompany
        {
            get { return _selectedCompany; }
            set 
            { 
                _selectedCompany = value;
                OnPorpertyChanged(nameof(SelectedCompany));
                OnPorpertyChanged(nameof(IsItemSelected));
            }
        }

        public bool IsItemSelected => SelectedCompany != null;




        /*
         * Commands
         */
        public ICommand NavigateToAddCompanyCommand { get; }
        public ICommand NavigateToUpsertCompanyCommand { get; }
        public ICommand NavigateToDetailsCompanyCommand { get; }

        public ICommand DeleteCompanyCommand { get; set; }







        public CompanyViewModel(
            UpsertCompanyViewModel upsertCompanyViewModel,
            CompanyDetailsViewModel companyDetailsViewModel,
            ICompanyRepository compRepo,
            INavigator navigator, 
            IDialogService dialogService)
        {
            _compRepo = compRepo;
            _companies = new ObservableCollection<Company>();

            _dialogService = dialogService;
            DeleteCompanyCommand = new ActionCommand(this, p => DeleteCompany(), o => IsItemSelected);

            GetCompanies();

            NavigateToAddCompanyCommand = new NavigateToAddCompanyCommand(this, upsertCompanyViewModel, navigator);
            NavigateToUpsertCompanyCommand = new NavigateToUpsertCompanyCommand(this, upsertCompanyViewModel, navigator);
            NavigateToDetailsCompanyCommand = new NavigateToDetailsCompanyCommand(this, companyDetailsViewModel, navigator);
        }

        private void GetCompanies()
        {
            IEnumerable<Company> companies = _compRepo.GetAll();

            foreach (Company company in companies)
            {
                _companies.Add(company);
            }
        }

        private async void DeleteCompany()
        {
            var viewModel = new YesCancelDialogViewModel(this, "Are you sure you want to delete this company?");

            bool? result = _dialogService.ShowDialog(viewModel);

            if (result.HasValue)
            {
                if (result.Value)
                {
                    await _compRepo.Remove(SelectedCompany.CompanyId);
                    _companies.Remove(SelectedCompany);
                    SelectedCompany = null;
                }
                else
                {
                    // Cancelled
                }
            }
        }
    }
}
