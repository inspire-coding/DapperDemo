using DapperDemo.WPF.Utils.DialogHelper;
using System;
using System.Windows.Input;

namespace DapperDemo.WPF.ViewModels.Dialog
{
    public class YesCancelDialogViewModel : IDialogRequestClose
    {
        public readonly ViewModelBase _viewModelBase;

        public YesCancelDialogViewModel(ViewModelBase viewModelBase, string message)
        {
            _viewModelBase = viewModelBase;

            Message = message;
            OkCommand = new ActionCommand(_viewModelBase, p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(true)));
            CancelCommand = new ActionCommand(_viewModelBase, p => CloseRequested?.Invoke(this, new DialogCloseRequestedEventArgs(false)));
        }


        public event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
        public string Message { get; }
        public ICommand OkCommand { get; }
        public ICommand CancelCommand { get; }
    }
}
