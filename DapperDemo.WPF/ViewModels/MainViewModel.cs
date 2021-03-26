using DapperDemo.Data.Repository;
using DapperDemo.WPF.Commands;
using DapperDemo.WPF.State.Navigators;
using DapperDemo.WPF.ViewModels.Factories;
using System.Windows.Input;

namespace DapperDemo.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IViewModelFactory _viewModelFactory;
        private readonly INavigator _navigator;


        public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;


        public ICommand UpdateCurrentViewModelCommand { get; }


        public MainViewModel(INavigator navigator, IViewModelFactory viewModelFactory)
        {
            _navigator = navigator;
            _viewModelFactory = viewModelFactory;

            _navigator.StateChanged += Navigator_StateChanged;

            UpdateCurrentViewModelCommand = new UpdateCurrentViewModelCommand(navigator, _viewModelFactory);
            UpdateCurrentViewModelCommand.Execute(ViewType.Home);
        }

        private void Navigator_StateChanged()
        {
            OnPorpertyChanged(nameof(CurrentViewModel));
        }
    }
}
