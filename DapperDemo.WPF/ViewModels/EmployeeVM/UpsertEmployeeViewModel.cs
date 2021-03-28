using DapperDemo.Data.Models;
using DapperDemo.Data.Repository;
using DapperDemo.WPF.Commands.CompanyCommands;
using DapperDemo.WPF.State.Navigators;
using DapperDemo.WPF.Utils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DapperDemo.WPF.ViewModels.EmployeeVM
{
    public class UpsertEmployeeViewModel : ViewModelBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPorpertyChanged(nameof(Name));
                OnPorpertyChanged(nameof(CanAddCompany));
                OnPorpertyChanged(nameof(Company));
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
                OnPorpertyChanged(nameof(CanAddCompany));
                OnPorpertyChanged(nameof(Company));
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
                OnPorpertyChanged(nameof(CanAddCompany));
                OnPorpertyChanged(nameof(Company));
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
                OnPorpertyChanged(nameof(CanAddCompany));
                OnPorpertyChanged(nameof(Company));
            }
        }

        private Company _company;
        public Company Company
        {
            get { return _company; }
            set
            {
                _company = value;
                OnPorpertyChanged(nameof(Company));
                OnPorpertyChanged(nameof(CanAddCompany));
                OnPorpertyChanged(nameof(Company));
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
                OnPorpertyChanged(nameof(SelectedEmployee));

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


        public Employee Employee => CompanyFactory();
        private Employee CompanyFactory()
        {
            return new Employee()
            {
                Name = this.Name,
                Email = this.Email,
                Phone = this.Phone,
                Title = this.Title,
                Company = this.Company
            };
        }

        public bool CanAddCompany => !string.IsNullOrEmpty(Name)
            && !string.IsNullOrEmpty(Email)
            && !string.IsNullOrEmpty(Phone)
            && !string.IsNullOrEmpty(Title);
            //&& !string.IsNullOrEmpty(Company);

        public ICommand BackToCompanyListCommand { get; }
        public ICommand UpsertCompanyCommand { get; }


        public UpsertEmployeeViewModel(IEmployeeRepository employeeRepo, IRenavigator navigateBackToCompanyView)
        {
            BackToCompanyListCommand = new BackToEmployeeListCommand(this, navigateBackToCompanyView);
            UpsertCompanyCommand = new UpsertEmployeeCommand(this, employeeRepo, UpsertAction, navigateBackToCompanyView);

            UpsertActionTitle = UpsertAction.Add.ToString();
        }

        private void UpdateProperties(Employee selectedCompany)
        {
            _name = selectedCompany.Name;
            _email = selectedCompany.Email;
            _phone = selectedCompany.Phone;
            _title = selectedCompany.Title;
            //_company = selectedCompany.CompanyId;
        }
    }
}
