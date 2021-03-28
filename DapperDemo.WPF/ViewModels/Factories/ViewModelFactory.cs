using DapperDemo.WPF.State.Navigators;
using DapperDemo.WPF.ViewModels.CompanyVM;
using DapperDemo.WPF.ViewModels.EmployeeVM;
using System;

namespace DapperDemo.WPF.ViewModels.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<HomeViewModel> _createHomeViewModel;
        private readonly CreateViewModel<CompanyViewModel> _createCompanyViewModel;
        private readonly CreateViewModel<UpsertCompanyViewModel> _createUpsertCompanyViewModel;
        private readonly CreateViewModel<EmployeesViewModel> _createEmployeesViewModel;
        private readonly CreateViewModel<UpsertEmployeeViewModel> _createUpsertEmployeeViewModel;

        public ViewModelFactory(
            CreateViewModel<HomeViewModel> createHomeViewModel, 
            CreateViewModel<CompanyViewModel> createCompanyViewModel, 
            CreateViewModel<UpsertCompanyViewModel> createUpsertCompanyViewModel, 
            CreateViewModel<EmployeesViewModel> createEmployeesViewModel, 
            CreateViewModel<UpsertEmployeeViewModel> createUpsertEmployeeViewModel)
        {
            _createHomeViewModel = createHomeViewModel;
            _createCompanyViewModel = createCompanyViewModel;
            _createUpsertCompanyViewModel = createUpsertCompanyViewModel;
            _createEmployeesViewModel = createEmployeesViewModel;
            _createUpsertEmployeeViewModel = createUpsertEmployeeViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType, object dataToBePassed = null)
        {
            return viewType switch
            {
                ViewType.Home => _createHomeViewModel(),
                ViewType.Company => _createCompanyViewModel(),
                ViewType.CreateCompany => _createUpsertCompanyViewModel(),
                ViewType.Employees => _createEmployeesViewModel(),
                ViewType.CreateEmployee => _createUpsertEmployeeViewModel(),
                _ => throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType"),
            };
        }
    }
}
