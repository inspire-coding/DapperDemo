using DapperDemo.Data.Data;
using DapperDemo.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace DapperDemo.WPF.HostBuilders
{
    public static class AddRepositoriesHostBuilderExtensions
    {
        public static IHostBuilder AddRepositories(this IHostBuilder host)
        {
            host.ConfigureServices(services =>
            {
                services.AddScoped<ICompanyRepository, CompanyRepositoryContrib>();
                services.AddScoped<IBonusRepository, BonusRepository>();
                services.AddScoped<IEmployeeRepository, EmployeeRepository>();
                services.AddScoped<IDapperSprocRepo, DapperSprocRepo>();
            });

            return host;
        }
    }
}
