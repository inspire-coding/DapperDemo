using DapperDemo.Data.Models;
using DapperDemo.Data.Repository;
using DapperDemo.WPF.Commands.Company;
using DapperDemo.WPF.Commands.CompanyCommands;
using DapperDemo.WPF.State.Navigators;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DapperDemo.WPF.ViewModels.CompanyVM
{
    public class CompanyViewModel : ViewModelBase
    {
        private readonly ICompanyRepository _compRepo;

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
            }
        }





        /*
         * Commands
         */
        public ICommand UpsertCompanyCommand { get; }





        public CompanyViewModel(ICompanyRepository compRepo, IRenavigator navigateToAddView)
        {
            _compRepo = compRepo;
            _companies = new ObservableCollection<Company>();

            GetCompanies();

            UpsertCompanyCommand = new UpsertCompanyCommand(this, navigateToAddView);
        }

        private void GetCompanies()
        {
            IEnumerable<Company> companies = _compRepo.GetAll();

            foreach (Company company in companies)
            {
                _companies.Add(company);
            }
        }
    }
}
