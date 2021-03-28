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




        private ICommand _closeCommand;
        public ICommand CloseComamnd
        {
            get
            {
                if (_closeCommand == null)
                {
                    _closeCommand = new RelayCommand(param => Close());
                }
                return _closeCommand;
            }
        }


        public void Close()
        {
           App.Current.Shutdown();
        }


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
