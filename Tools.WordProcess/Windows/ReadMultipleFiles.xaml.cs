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
using System.Windows.Shapes;

namespace Tools.WordProcess.Windows
{
    /// <summary>
    /// Interaction logic for ReadMultipleFiles.xaml
    /// </summary>
    public partial class ReadMultipleFiles : Window
    {

        private class ReadMultipleFilesViewModel
        {
            public string Folder { get; set; } = @"C:\Users\matthew\Downloads";

            public int StartFile { get; set; } = 1;

            public int EndFile { get; set; } = 1;

            public string ExtensionString { get; set; } = ".txt";

            public string Import()
            {
                var text = new StringBuilder();

                for (int i = StartFile; i <= EndFile; i++)
                {
                    var filename = Folder + @"\" + i + ExtensionString;
                    text.Append(System.IO.File.ReadAllText(filename));
                }

                return text.ToString();
            }
        }

        private ReadMultipleFilesViewModel mVieModel;

        public string Text { get; set; }


        public ReadMultipleFiles()
        {
            InitializeComponent();

            mVieModel = new ReadMultipleFilesViewModel();
            this.DataContext = mVieModel;
        }


        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            Text = mVieModel.Import();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

    }
}
