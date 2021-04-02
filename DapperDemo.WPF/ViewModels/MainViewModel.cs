using DapperDemo.WPF.Commands;
using DapperDemo.WPF.State.Navigators;
using DapperDemo.WPF.Utils.DialogHelper;
using DapperDemo.WPF.ViewModels.Factories;
using System.Windows;
using System.Windows.Input;

namespace DapperDemo.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IViewModelFactory _viewModelFactory;
        private readonly INavigator _navigator;


        private bool _isChecked;
        public bool IsChecked
        {
            get 
            { 
                return _isChecked;
            }
            set 
            { 
                _isChecked = value;
                OnPorpertyChanged(nameof(IsChecked));
                IsEnabled = !_isChecked;
                ChangeOpacityProperty(IsChecked);
            }
        }


        private bool _isEnabled = true;
        public bool IsEnabled
        {
            get 
            { 
                return _isEnabled;
            }
            set 
            {
                _isEnabled = value;
                OnPorpertyChanged(nameof(IsEnabled));
            }
        }


        private Visibility _visibility;
        public Visibility Visibility
        {
            get { return _visibility; }
            set 
            { 
                _visibility = value;
                OnPorpertyChanged(nameof(Visibility));
            }
        }



        public ViewModelBase CurrentViewModel => _navigator.CurrentViewModel;


        private double _opacity = 1;
        public double Opacity
        {
            get 
            { 
                return _opacity;
            }
            set 
            { 
                _opacity = value;
                OnPorpertyChanged(nameof(Opacity));
            }
        }





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
        public ICommand CloseNavigationDrawerComamnd { get; }




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

            CloseNavigationDrawerComamnd = new ActionCommand(this, method => CloseNavigationDrawer());
        }

        private void ChangeOpacityProperty(bool isChecked)
        {
            if (isChecked)
            {
                Opacity = 0.3;
            }
            else
            {
                Opacity = 1;
            }
        }

        private void CloseNavigationDrawer()
        {
            IsChecked = false;
            IsEnabled = true;
        }

        private void Navigator_StateChanged()
        {
            OnPorpertyChanged(nameof(CurrentViewModel));
        }
    }
}
