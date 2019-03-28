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
using System.Windows.Shapes;
using TMTech.Shared.WPFLIB.ViewModels;

namespace Tools.WordProcess.Windows
{
    /// <summary>
    /// Interaction logic for ImportFromFile.xaml
    /// </summary>
    public partial class ImportFromFile : Window
    {
        private class FileConversionViewModel : BaseViewModel
        {
            public string Filename { get; private set; }

            public EncodingType mEncodingType = EncodingType.WindowsDefault;
            public EncodingType EncodingType {
                get { return mEncodingType; }
                set
                {
                    if (mEncodingType != value)
                    {
                        mEncodingType = value;
                        NotifyPropertyChanged();
                        PreviewFile();
                    }
                }
            }

            public IEnumerable<EncodingInfo> AllEncodings { get; set; }

            private EncodingInfo mSelectedEncoding;
            public EncodingInfo SelectedEncoding {
                get { return mSelectedEncoding; }
                set
                {
                    if (mSelectedEncoding != value)
                    {
                        mSelectedEncoding = value;
                        NotifyPropertyChanged();
                        this.PreviewFile();
                    }
                }
            }

            private string mPreviewText;
            public string PreviewText {
                get
                {
                    return this.mPreviewText;
                }
                set
                {
                    this.mPreviewText = value;
                    NotifyPropertyChanged();
                }
            }

            /// <summary>
            /// Constructor
            /// </summary>
            /// <param name="filename"></param>
            public FileConversionViewModel(string filename)
            {
                this.Filename = filename;

                this.AllEncodings = Encoding.GetEncodings().OrderBy(x => x.DisplayName);
                
                // read a small part of the text file
                PreviewFile();
            }

            public void PreviewFile()
            {
                StreamReader reader;
                if (this.SelectedEncoding == null)
                {
                    reader = new StreamReader(this.Filename, Encoding.Default);
                }
                else
                {
                    reader = new StreamReader(this.Filename, this.SelectedEncoding.GetEncoding());
                }

                int lines = 0;
                string text = string.Empty;
                do
                {
                    lines++;
                    text = text + reader.ReadLine() + "\r";
                } while (!reader.EndOfStream && lines < 10);

                this.PreviewText = text;
            }

            /// <summary>
            /// Read the file using the selected encoding.
            /// </summary>
            /// <returns></returns>
            public string Process()
            {
                return File.ReadAllText(this.Filename, this.SelectedEncoding.GetEncoding());
            }
        }

        private FileConversionViewModel mViewModel;

        public ImportFromFile(string filename)
        {
            InitializeComponent();

            mViewModel = new FileConversionViewModel(filename);
            this.DataContext = mViewModel;
        }

        private void Process_Click(object sender, RoutedEventArgs e)
        {
            Process();
            MessageBox.Show("Done!");
            this.Close();
        }

        private void Process()
        {
            var text = mViewModel.Process();
            var result = WordProcessTool.RemoveEmptyLine(text);
            Clipboard.SetText(result);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
