using DapperDemo.Data.Models;
using DapperDemo.Data.Repository;
using DapperDemo.WPF.Commands.CompanyCommands;
using DapperDemo.WPF.State.Navigators;
using DapperDemo.WPF.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace DapperDemo.WPF.ViewModels.EmployeeVM
{
    public class UpsertEmployeeViewModel : ViewModelBase
    {
        private readonly ICompanyRepository _companyRepo;


        private readonly ObservableCollection<Company> _companies;
        public IEnumerable<Company> Companies => _companies;





        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPorpertyChanged(nameof(Name));
                OnPorpertyChanged(nameof(CanAddEmployee));
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPorpertyChanged(nameof(Email));
                OnPorpertyChanged(nameof(CanAddEmployee));
            }
        }

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPorpertyChanged(nameof(Phone));
                OnPorpertyChanged(nameof(CanAddEmployee));
            }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPorpertyChanged(nameof(Title));
                OnPorpertyChanged(nameof(CanAddEmployee));
            }
        }

        private Company _selectedCompany;
        public Company SelectedCompany
        {
            get { return _selectedCompany; }
            set
            {
                _selectedCompany = value;
                OnPorpertyChanged(nameof(SelectedCompany));
                OnPorpertyChanged(nameof(CanAddEmployee));
            }
        }

        private Employee _selectedEmployee;
        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                _selectedEmployee = value;
                if (_selectedEmployee != null) UpdateProperties(_selectedEmployee);

                if (_selectedEmployee == null)
                {
                    UpsertAction = UpsertAction.Add;
                    UpsertActionTitle = UpsertAction.Add.ToString();
                }
                else
                {
                    UpsertAction = UpsertAction.Update;
                    UpsertActionTitle = UpsertAction.Update.ToString();
                }
                OnPorpertyChanged(nameof(UpsertAction));
                OnPorpertyChanged(nameof(UpsertActionTitle));
                OnPorpertyChanged(nameof(SelectedEmployee));
                OnPorpertyChanged(nameof(SelectedCompany));
                OnPorpertyChanged(nameof(CanAddEmployee));
            }
        }


        private UpsertAction _upsertAction;
        public UpsertAction UpsertAction
        {
            get { return _upsertAction; }
            set
            {
                _upsertAction = value;
                OnPorpertyChanged(nameof(UpsertAction));
            }
        }
        private string _upsertActionTitle;
        public string UpsertActionTitle
        {
            get { return _upsertActionTitle + " Employee"; }
            set
            {
                _upsertActionTitle = value;
                OnPorpertyChanged(nameof(UpsertActionTitle));
            }
        }


        public Employee Employee => EmployeeFactory();
        private Employee EmployeeFactory()
        {
            return new Employee()
            {
                Name = this.Name,
                Email = this.Email,
                Phone = this.Phone,
                Title = this.Title,
                Company = SelectedCompany
            };
        }


        public bool CanAddEmployee => !string.IsNullOrEmpty(Name)
            && !string.IsNullOrEmpty(Email)
            && !string.IsNullOrEmpty(Phone)
            && !string.IsNullOrEmpty(Title)
            && SelectedCompany != null;





        public ICommand BackToCompanyListCommand { get; }
        public ICommand UpsertCompanyCommand { get; }





        public UpsertEmployeeViewModel(IEmployeeRepository employeeRepo, ICompanyRepository companyRepo, IRenavigator navigateBackToCompanyView)
        {
            _companyRepo = companyRepo;

            _companies = new ObservableCollection<Company>();

            UploadCompaniesComboBox();

            BackToCompanyListCommand = new BackToEmployeeListCommand(this, navigateBackToCompanyView);
            UpsertCompanyCommand = new UpsertEmployeeCommand(this, employeeRepo, UpsertAction, navigateBackToCompanyView);

            UpsertActionTitle = UpsertAction.Add.ToString();
        }




        private void UpdateProperties(Employee selectedEmployee)
        {
            _name = selectedEmployee.Name;
            _email = selectedEmployee.Email;
            _phone = selectedEmployee.Phone;
            _title = selectedEmployee.Title;

            _selectedCompany = _companies
                .Where(company => company.CompanyId == selectedEmployee.CompanyId)
                .FirstOrDefault();
        }

        private void UploadCompaniesComboBox()
        {
            IEnumerable<Company> companyList = _companyRepo.GetAll();
            foreach (Company item in companyList)
            {
                _companies.Add(item);
            }
        }
    }
}
