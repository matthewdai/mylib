using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TMTech.Shared.WPFLIB.Controls;

namespace TestBorderlessWindow
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : BorderlessWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Testing");
        }

        private void TestMessageDlg_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new MessageDialog();
            dlg.ShowMsg("It's very easy to show this dialog as you only need to set the Visibility of the InputBox grid to visible. You then simply handle the Yes / No buttons and get the Input text from the TextBox. So instead of using code that requires ShowDialog(), you simply set the Visibility option to Visible.There are still some things to do in this example that we will handle in code - behind, like for example clearing the InputText box after handling the Yes / No Button clicks.");
        }
    }
}
