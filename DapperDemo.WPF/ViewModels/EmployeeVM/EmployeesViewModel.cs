using DapperDemo.Data.Models;
using DapperDemo.Data.Repository;
using DapperDemo.WPF.Commands.Company;
using DapperDemo.WPF.Commands.CompanyCommands;
using DapperDemo.WPF.State.Navigators;
using DapperDemo.WPF.Utils.DialogHelper;
using DapperDemo.WPF.ViewModels.Dialog;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace DapperDemo.WPF.ViewModels.EmployeeVM
{
    public class EmployeesViewModel : ViewModelBase
    {
        private readonly IEmployeeRepository _empRepo;
        private readonly IBonusRepository _bonusRepo;
        private readonly IDialogService _dialogService;



        private readonly ObservableCollection<Employee> _employees;
        public IEnumerable<Employee> Employees => _employees;


        /*
         * Properties
         */
        private Employee _selectedEmployee;

        public Employee SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                _selectedEmployee = value;
                OnPorpertyChanged(nameof(SelectedEmployee));
                OnPorpertyChanged(nameof(IsItemSelected));
            }
        }

        public bool IsItemSelected => SelectedEmployee != null;




        /*
         * Commands
         */
        public ICommand NavigateToAddEmployeeCommand { get; }
        public ICommand NavigateToUpsertEmployeeCommand { get; }

        public ICommand DeleteEmployeeCommand { get; set; }







        public EmployeesViewModel(
            UpsertEmployeeViewModel upsertEmployeeViewModel,
            IEmployeeRepository empRepo,
            IBonusRepository bonusRepo,
            INavigator navigator,
            IDialogService dialogService)
        {
            _empRepo = empRepo;
            _bonusRepo = bonusRepo;
            _employees = new ObservableCollection<Employee>();

            _dialogService = dialogService;
            DeleteEmployeeCommand = new ActionCommand(this, p => DeleteCompany(), o => IsItemSelected);

            GetCompanies();

            NavigateToAddEmployeeCommand = new NavigateToAddEmployeeCommand(this, upsertEmployeeViewModel, navigator);
            NavigateToUpsertEmployeeCommand = new NavigateToUpsertEmployeeCommand(this, upsertEmployeeViewModel, navigator);
        }

        private async void GetCompanies()
        {
            IEnumerable<Employee> companies = await _bonusRepo.GetEmployeeWithCompany(0);

            foreach (Employee company in companies)
            {
                _employees.Add(company);
            }
        }

        private async void DeleteCompany()
        {
            var viewModel = new YesCancelDialogViewModel(this, "Are you sure you want to delete this employee?");

            bool? result = _dialogService.ShowDialog(viewModel);

            if (result.HasValue)
            {
                if (result.Value)
                {
                    await _empRepo.Remove(SelectedEmployee.EmployeeId);
                    _employees.Remove(SelectedEmployee);
                    SelectedEmployee = null;
                }
                else
                {
                    // Cancelled
                }
            }
        }
    }
}
