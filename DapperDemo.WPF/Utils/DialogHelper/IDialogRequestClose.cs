using System;
using System.Collections.Generic;
using System.Text;

namespace DapperDemo.WPF.Utils.DialogHelper
{
    public interface IDialogRequestClose
    {
        event EventHandler<DialogCloseRequestedEventArgs> CloseRequested;
    }
}
