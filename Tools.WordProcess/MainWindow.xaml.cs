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

namespace Tools.WordProcess
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Process_Click(object sender, RoutedEventArgs e)
        {
            ProcessWord();
        }


        private void ProcessWord()
        {
            // get source text
            var source = this.textSource.Text;

            var result = WordProcessTool.RemoveEmptyLine(source);

            // clear target box
            this.textTarget.Clear();

            // put result in the target box
            this.textTarget.Text = result;

            // also copy result to clipborder
            Clipboard.SetText(result);

        }
    }
}
