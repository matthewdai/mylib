using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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



        private void ImportFile(string filename, string outputFilename)
        {

            using (var sr = new StreamReader(filename, Encoding.GetEncoding(936)))
            {
                var writer = new StreamWriter(outputFilename);

                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    line = line.Trim();

                    if (!string.IsNullOrEmpty(line))
                    {
                        writer.WriteLine(line);
                    }
                }

                writer.Close();
            }

        }


        private void ProcessFile_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.CheckFileExists = true;
            string filename;

            if (dlg.ShowDialog() == true)
            {
                filename = dlg.FileName;
            }
            else
                return;

            // process file
            ImportFile(filename, filename + ".txt");


        }
    }
}
