using DapperDemo.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Text;

namespace DapperDemo.WPF.ViewModels.Factories
{
    public interface IViewModelFactory
    {
        ViewModelBase CreateViewModel(ViewType viewType, object dataToBePassed = null);
    }
}
