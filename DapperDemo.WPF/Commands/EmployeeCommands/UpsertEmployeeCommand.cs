using DapperDemo.Data.Repository;
using DapperDemo.WPF.State.Navigators;
using DapperDemo.WPF.Utils;
using DapperDemo.WPF.ViewModels.EmployeeVM;
using System;
using System.ComponentModel;
using System.Windows.Input;

namespace DapperDemo.WPF.Commands.CompanyCommands
{
    public class UpsertEmployeeCommand : ICommand
    {
        private readonly IEmployeeRepository _employeeRepo;
        public readonly UpsertEmployeeViewModel _upsertEmployeeViewModel;
        private readonly IRenavigator _renavigator;
        private readonly UpsertAction _upsertAction;

        public UpsertEmployeeCommand(UpsertEmployeeViewModel upsertEmployeeViewModel, IEmployeeRepository employeeRepo, UpsertAction upsertAction, IRenavigator renavigator)
        {
            _employeeRepo = employeeRepo;
            _upsertEmployeeViewModel = upsertEmployeeViewModel;
            _renavigator = renavigator;
            _upsertAction = upsertAction;

            upsertEmployeeViewModel.PropertyChanged += CreateCompanyViewModel_PropertyChanged;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _upsertEmployeeViewModel.CanAddEmployee;
        }

        public async void Execute(object parameter)
        {
            switch (_upsertAction)
            {
                case UpsertAction.Add:
                    await _employeeRepo.AddAsync(_upsertEmployeeViewModel.Employee);
                    break;
                case UpsertAction.Update:
                    await _employeeRepo.Update(_upsertEmployeeViewModel.Employee);
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
