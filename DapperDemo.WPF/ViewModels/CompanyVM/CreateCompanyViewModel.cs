using DapperDemo.Data.Repository;
using DapperDemo.WPF.Commands.Company;
using DapperDemo.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DapperDemo.WPF.ViewModels.CompanyVM
{
    public class CreateCompanyViewModel : ViewModelBase
    {
        private readonly ICompanyRepository _compRepo;

        public ICommand BackToListCommand { get; }


        public CreateCompanyViewModel(ICompanyRepository compRepo, IRenavigator navigateBackToCompanyView)
        {
            _compRepo = compRepo;

            BackToListCommand = new BackToListCommand(this, navigateBackToCompanyView);
        }
    }
}
