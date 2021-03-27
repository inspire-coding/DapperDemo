using DapperDemo.Data.Models;
using DapperDemo.Data.Repository;
using DapperDemo.WPF.Commands.CompanyCommands;
using DapperDemo.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace DapperDemo.WPF.ViewModels.CompanyVM
{
    public class CompanyDetailsViewModel : ViewModelBase
    {
        private readonly IBonusRepository _bonusRepo;

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPorpertyChanged(nameof(Name));
            }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPorpertyChanged(nameof(Address));
            }
        }

        private string _city;
        public string City
        {
            get { return _city; }
            set
            {
                _city = value;
                OnPorpertyChanged(nameof(City));
            }
        }

        private string _state;
        public string State
        {
            get { return _state; }
            set
            {
                _state = value;
                OnPorpertyChanged(nameof(State));
            }
        }

        private string _postalCode;
        public string PostalCode
        {
            get { return _postalCode; }
            set
            {
                _postalCode = value;
                OnPorpertyChanged(nameof(PostalCode));
            }
        }

        private readonly ObservableCollection<Employee> _employees;
        public IEnumerable<Employee> Employees => _employees;


        private int _selectedCompanyId;

        public int SelectedCompanyId
        {
            get { return _selectedCompanyId; }
            set 
            { 
                _selectedCompanyId = value;
                GetCompanyWithEmployees();
                OnPorpertyChanged(nameof(SelectedCompanyId));
                OnPorpertyChanged(nameof(SelectedCompany));
            }
        }





        private Company _selectedCompany;
        public Company SelectedCompany
        {
            get { return _selectedCompany; }
            set
            {
                _selectedCompany = value;
                if (_selectedCompany != null) UpdateProperties(_selectedCompany);
                OnPorpertyChanged(nameof(SelectedCompany));
            }
        }
        


        public ICommand BackToListCommand { get; }

        public CompanyDetailsViewModel(IRenavigator navigateBackToCompanyView, IBonusRepository bonusRepo)
        {
            _employees = new ObservableCollection<Employee>();
            _bonusRepo = bonusRepo;

            BackToListCommand = new BackToCompanyListCommand(this, navigateBackToCompanyView);
        }


        private void GetCompanyWithEmployees()
        {
            SelectedCompany = _bonusRepo.GetCompanyWithEmployees(SelectedCompanyId);
        }


        private void UpdateProperties(Company selectedCompany)
        {
            _name = selectedCompany.Name;
            _address = selectedCompany.Address;
            _city = selectedCompany.City;
            _state = selectedCompany.State;
            _postalCode = selectedCompany.PostalCode;

            foreach (Employee employee in selectedCompany.Employees)
            {
                _employees.Add(employee);
            }
        }
    }
}
