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

namespace Tools.FilenameChanger.Controls
{
    /// <summary>
    /// Interaction logic for WordProcessor.xaml
    /// </summary>
    public partial class WordProcessor : UserControl
    {
        public WordProcessor()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var text = this.textBox.Text;

            this.textBox.Text = RemoveEmptyLines(text);
        }


        private string RemoveEmptyLines(string text)
        {
            var sepatator = new string[] { "\n", "\r\n" };

            var lines = text.Split(sepatator, StringSplitOptions.RemoveEmptyEntries);

            var result = new StringBuilder();
            
            foreach (var line in lines)
            {
                var trimedLine = line.Trim();

                if (!string.IsNullOrEmpty(trimedLine))
                {
                    result.Append(trimedLine + "\n");
                }
            }

            return result.ToString();
        }

    }
}
