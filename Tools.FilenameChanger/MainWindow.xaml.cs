using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tools.FilenameChanger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ViewModel _ViewModel;



        public MainWindow()
        {
            InitializeComponent();

            _ViewModel = new ViewModel();

            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = _ViewModel;
        }





        public abstract class ViewModelBase : INotifyPropertyChanged
        {
            /// <summary>
            /// Define the property changed events
            /// </summary>
            public event PropertyChangedEventHandler PropertyChanged;

            public void OnPropertyChanged(string name)
            {
                PropertyChangedEventHandler handler = PropertyChanged;
                if (handler != null)
                    handler(this, new PropertyChangedEventArgs(name));
            }
        }



        public class ViewModel : ViewModelBase
        {
            private string _Folder;
            public string Folder
            {
                get
                {
                    return _Folder;
                }
                set
                {
                    if (_Folder != value)
                    {
                        _Folder = value;
                        OnPropertyChanged("Folder");
                        FetchFilenames(_Folder);
                    }
                    
                }
            }

            private string _Format;
            public string Format
            {
                get
                {
                    return _Format;
                }
                set
                {
                    if (_Format != value)
                    {
                        _Format = value;
                        RefreshFileNames();
                    }
                }
            }

            private int _StartingNumber;
            public int StartingNumber
            {
                get { return _StartingNumber; }
                set
                {   
                    if (_StartingNumber != value)
                    {
                        _StartingNumber = value;
                        RefreshFileNames();
                    }
                }
            }
            public ObservableCollection<FileViewModel> Files { get; set; }

            /// <summary>
            /// Constructor
            /// </summary>
            public ViewModel()
            {
                Files = new ObservableCollection<FileViewModel>();
                StartingNumber = 1;
                Format = "{0}";
            }

                 

            private void FetchFilenames(string path)
            {
                string[] files = System.IO.Directory.GetFiles(path);

                Files.Clear();
                int count = StartingNumber;
                foreach (var item in files)
                {
                    Files.Add(new FileViewModel() {
                        FullPath = item,
                        FileName = System.IO.Path.GetFileName(item),
                        NewFileName = string.Format(Format, count++) + System.IO.Path.GetExtension(item),
                    });
                }
            }


            public void Refresh()
            {
                FetchFilenames(Folder);
            }


            private void RefreshFileNames()
            {
                int count = StartingNumber;
                foreach (var item in Files)
                {
                    item.NewFileName = string.Format(Format, count++) + System.IO.Path.GetExtension(item.FileName);
                }
            }


            /// <summary>
            /// Rename file names
            /// </summary>
            public void Rename()
            {
                foreach (var item in Files)
                {
                    string newPath = Folder + @"\" + item.NewFileName;
                    System.IO.File.Move(item.FullPath, newPath);
                }

                FetchFilenames(Folder);
            }
        }


        public class FileViewModel : ViewModelBase
        {
            public string FullPath { get; set; }
            public string FileName { get; set; }

            private string _NewFileName;
            public string NewFileName
            {
                get
                {
                    return _NewFileName;
                }
                set
                {
                    if (_NewFileName != value)
                    {
                        _NewFileName = value;
                        OnPropertyChanged("NewFileName");
                    }
                }
            }                     

        }

        private void DataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = (e.Row.GetIndex() + 1).ToString();
        }

        private void Browse_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new FolderBrowserDialog();
            dlg.SelectedPath = _ViewModel.Folder;

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                _ViewModel.Folder = dlg.SelectedPath;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Update_Click(object sender, RoutedEventArgs e) {
            // start update
            _ViewModel.Rename();

        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            _ViewModel.Refresh();
        }
    }
}
