using DapperDemo.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DapperDemo.WPF.State.Navigators
{
    public enum ViewType
    {
        Home,
        Company,
        CreateCompany
    }

    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
        event Action StateChanged;
    }
}
