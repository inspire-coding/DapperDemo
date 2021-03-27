using DapperDemo.Data.Models;
using DapperDemo.Data.Repository;
using DapperDemo.WPF.Commands.CompanyCommands;
using DapperDemo.WPF.State.Navigators;
using DapperDemo.WPF.Utils;
using System.Windows.Input;

namespace DapperDemo.WPF.ViewModels.CompanyVM
{
    public class UpsertCompanyViewModel : ViewModelBase
    {
        private readonly ICompanyRepository _compRepo;



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

        private string _address;
        public string Address
        {
            get { return _address; }
            set 
            {
                _address = value;
                OnPorpertyChanged(nameof(Address));
                OnPorpertyChanged(nameof(CanAddCompany));
                OnPorpertyChanged(nameof(Company));
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
                OnPorpertyChanged(nameof(CanAddCompany));
                OnPorpertyChanged(nameof(Company));
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
                OnPorpertyChanged(nameof(CanAddCompany));
                OnPorpertyChanged(nameof(Company));
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
                OnPorpertyChanged(nameof(CanAddCompany));
                OnPorpertyChanged(nameof(Company));
            }
        }

        private Company _selectedCompany;
        public Company SelectedCompany
        {
            get { return _selectedCompany; }
            set
            {
                _selectedCompany = value;
                if(_selectedCompany != null) UpdateProperties(_selectedCompany);
                OnPorpertyChanged(nameof(SelectedCompany));

                if (_selectedCompany == null)
                {
                    _upsertAction = UpsertAction.Add;
                }
                else
                {
                    _upsertAction = UpsertAction.Update;
                }
                OnPorpertyChanged(nameof(UpsertAction));
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


        public Company Company => CompanyFactory();
        private Company CompanyFactory()
        {
            return new Company()
            {
                Name = this.Name,
                Address = this.Address,
                City = this.City,
                State = this.State,
                PostalCode = this.PostalCode
            };
        }

        public bool CanAddCompany => !string.IsNullOrEmpty(Name)
            && !string.IsNullOrEmpty(Address)
            && !string.IsNullOrEmpty(City)
            && !string.IsNullOrEmpty(State)
            && !string.IsNullOrEmpty(PostalCode);

        public ICommand BackToCompanyListCommand { get; }
        public ICommand UpsertCompanyCommand { get; }


        public UpsertCompanyViewModel(ICompanyRepository compRepo, IRenavigator navigateBackToCompanyView)
        {
            _compRepo = compRepo;

            BackToCompanyListCommand = new BackToCompanyListCommand(this, navigateBackToCompanyView);
            UpsertCompanyCommand = new UpsertCompanyCommand(this, compRepo, UpsertAction, navigateBackToCompanyView);
        }

        private void UpdateProperties(Company selectedCompany)
        {
            _name = selectedCompany.Name;
            _address = selectedCompany.Address;
            _city = selectedCompany.City;
            _state = selectedCompany.State;
            _postalCode = selectedCompany.PostalCode;
        }
    }
}
