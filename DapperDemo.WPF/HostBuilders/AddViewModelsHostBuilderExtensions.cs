﻿using DapperDemo.Data.Repository;
using DapperDemo.WPF.State.Navigators;
using DapperDemo.WPF.ViewModels;
using DapperDemo.WPF.ViewModels.CompanyVM;
using DapperDemo.WPF.ViewModels.Factories;
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
                services.AddTransient<MainViewModel>();
                services.AddTransient<HomeViewModel>();
                services.AddTransient<CompanyViewModel>();
                services.AddTransient(CreateCreateCompanyViewModel);

                services.AddSingleton<CreateViewModel<HomeViewModel>>(services => () => services.GetRequiredService<HomeViewModel>());
                services.AddSingleton<CreateViewModel<CompanyViewModel>>(services => () => CreateCompanyViewModel(services));
                services.AddSingleton<CreateViewModel<CreateCompanyViewModel>>(services => () => CreateCreateCompanyViewModel(services));

                services.AddSingleton<IViewModelFactory, ViewModelFactory>();

                services.AddSingleton<ViewModelDelegateRenavigator<CompanyViewModel>>();
                services.AddSingleton<ViewModelDelegateRenavigator<CreateCompanyViewModel>>();
            });

            return host;
        }

        private static CompanyViewModel CreateCompanyViewModel(IServiceProvider services)
        {
            return new CompanyViewModel(
                services.GetRequiredService<ICompanyRepository>(), 
                services.GetRequiredService<ViewModelDelegateRenavigator<CreateCompanyViewModel>>());
        }

        private static CreateCompanyViewModel CreateCreateCompanyViewModel(IServiceProvider service)
        {
            return new CreateCompanyViewModel(
                service.GetRequiredService<ICompanyRepository>(),
                service.GetRequiredService<ViewModelDelegateRenavigator<CompanyViewModel>>());
        }
    }
}
