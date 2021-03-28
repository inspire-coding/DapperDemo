using DapperDemo.Data.Repository;
using DapperDemo.WPF.State.Navigators;
using DapperDemo.WPF.Utils.DialogHelper;
using DapperDemo.WPF.ViewModels;
using DapperDemo.WPF.ViewModels.CompanyVM;
using DapperDemo.WPF.ViewModels.Dialog;
using DapperDemo.WPF.ViewModels.EmployeeVM;
using DapperDemo.WPF.ViewModels.Factories;
using DapperDemo.WPF.Views.Dialog;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace DapperDemo.WPF.HostBuilders
{
    public static class AddViewModelsHostBuilderExtensions
    {
        public static IHostBuilder AddViewModels(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddTransient(CreateCompanyViewModel);
                services.AddTransient(CreateEmployeesViewModel);
                services.AddTransient<MainViewModel>();
                services.AddTransient<HomeViewModel>();
                services.AddTransient<CompanyViewModel>();
                services.AddTransient(CreateUpsertCompanyViewModel);
                services.AddTransient(CreateCompanyDetailsViewModel);
                services.AddTransient(CreateUpsertEmployeeViewModel);

                services.AddSingleton<CreateViewModel<HomeViewModel>>(services => () => services.GetRequiredService<HomeViewModel>());
                services.AddSingleton<CreateViewModel<CompanyViewModel>>(services => () => CreateCompanyViewModel(services));
                services.AddSingleton<CreateViewModel<EmployeesViewModel>>(services => () => CreateEmployeesViewModel(services));
                services.AddSingleton<CreateViewModel<UpsertCompanyViewModel>>(services => () => CreateUpsertCompanyViewModel(services));
                services.AddSingleton<CreateViewModel<UpsertEmployeeViewModel>>(services => () => CreateUpsertEmployeeViewModel(services));
                services.AddSingleton<CreateViewModel<CompanyDetailsViewModel>>(services => () => CreateCompanyDetailsViewModel(services));

                services.AddSingleton<IViewModelFactory, ViewModelFactory>();

                services.AddSingleton<ViewModelDelegateRenavigator<CompanyViewModel>>();
                services.AddSingleton<ViewModelDelegateRenavigator<UpsertCompanyViewModel>>();
                services.AddSingleton<ViewModelDelegateRenavigator<CompanyDetailsViewModel>>();
                services.AddSingleton<ViewModelDelegateRenavigator<EmployeesViewModel>>();
                services.AddSingleton<ViewModelDelegateRenavigator<UpsertEmployeeViewModel>>();
            });

            return host;
        }

        private static CompanyViewModel CreateCompanyViewModel(IServiceProvider services)
        {
            IDialogService dialogService = new DialogService(services.GetRequiredService<MainWindow>());
            dialogService.Register<YesCancelDialogViewModel, YesCancelDialog>();

            return new CompanyViewModel(
                services.GetRequiredService<UpsertCompanyViewModel>(),
                services.GetRequiredService<CompanyDetailsViewModel>(),
                services.GetRequiredService<ICompanyRepository>(), 
                services.GetRequiredService<INavigator>(),
                dialogService
                );
        }

        private static UpsertCompanyViewModel CreateUpsertCompanyViewModel(IServiceProvider services)
        {
            return new UpsertCompanyViewModel(
                services.GetRequiredService<ICompanyRepository>(),
                services.GetRequiredService<ViewModelDelegateRenavigator<CompanyViewModel>>());
        }

        private static CompanyDetailsViewModel CreateCompanyDetailsViewModel(IServiceProvider services)
        {
            return new CompanyDetailsViewModel(
                services.GetRequiredService<ViewModelDelegateRenavigator<CompanyViewModel>>(),
                services.GetRequiredService<IBonusRepository>());
        }

        private static EmployeesViewModel CreateEmployeesViewModel(IServiceProvider services)
        {
            IDialogService dialogService = new DialogService(services.GetRequiredService<MainWindow>());
            dialogService.Register<YesCancelDialogViewModel, YesCancelDialog>();

            return new EmployeesViewModel(
                services.GetRequiredService<UpsertEmployeeViewModel>(),
                services.GetRequiredService<IEmployeeRepository>(),
                services.GetRequiredService<IBonusRepository>(),
                services.GetRequiredService<INavigator>(),
                dialogService
                );
        }

        private static UpsertEmployeeViewModel CreateUpsertEmployeeViewModel(IServiceProvider services)
        {
            return new UpsertEmployeeViewModel(
                services.GetRequiredService<IEmployeeRepository>(),
                services.GetRequiredService<ViewModelDelegateRenavigator<EmployeesViewModel>>());
        }
    }
}
