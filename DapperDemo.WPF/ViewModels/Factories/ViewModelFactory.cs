using DapperDemo.WPF.State.Navigators;
using DapperDemo.WPF.ViewModels.CompanyVM;
using System;

namespace DapperDemo.WPF.ViewModels.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<HomeViewModel> _createHomeViewModel;
        private readonly CreateViewModel<CompanyViewModel> _createCompanyViewModel;
        private readonly CreateViewModel<CreateCompanyViewModel> _createCreateCompanyViewModel;

        public ViewModelFactory(CreateViewModel<HomeViewModel> createHomeViewModel, CreateViewModel<CompanyViewModel> createCompanyViewModel, CreateViewModel<CreateCompanyViewModel> createCreateCompanyViewModel)
        {
            _createHomeViewModel = createHomeViewModel;
            _createCompanyViewModel = createCompanyViewModel;
            _createCreateCompanyViewModel = createCreateCompanyViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            return viewType switch
            {
                ViewType.Home => _createHomeViewModel(),
                ViewType.Company => _createCompanyViewModel(),
                ViewType.CreateCompany => _createCreateCompanyViewModel(),
                _ => throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType"),
            };
        }
    }
}
