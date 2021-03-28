using DapperDemo.WPF.Utils.DialogHelper;
using System.Windows;
using System.Windows.Input;

namespace DapperDemo.WPF.Views.Dialog
{
    /// <summary>
    /// Interaction logic for YesCancelDialog.xaml
    /// </summary>
    public partial class YesCancelDialog : Window, IDialog
    {
        public YesCancelDialog()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
