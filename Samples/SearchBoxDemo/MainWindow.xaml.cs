using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SearchBoxDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var vm = new DemoViewModel();
            vm.PolicyName = vm.AllNames[1];

            this.DataContext = vm;

        }

        public class DemoViewModel : ViewModelBase
        {
            public string SearchText { get; set; }


            private string mPolicyName;
            public string PolicyName
            {
                get { return mPolicyName; }
                set
                {
                    if (mPolicyName != value)
                    {
                        mPolicyName = value;
                        NotifyPropertyChanged();

                        SearchText = value;

                        //CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listView.ItemsSource);
                        //var collectionViewSource = CollectionViewSource.GetDefaultView(AllNames);
                        //collectionViewSource.Filter += a =>
                        //{
                        //    var s = a.ToString();
                        //    if (s.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) != -1)
                        //    {
                        //        return true;
                        //    }
                        //    return false;
                        //};
                    }

                }
            }


            private ObservableCollection<string> mAllNames = new ObservableCollection<string>() { "aaa", "asdfa", "hhh", "hjj", };
            public ObservableCollection<string> AllNames
            {
                get { return mAllNames; }
            }
            
        }

        private void SearchTextBox_Search(object sender, RoutedEventArgs e)
        {
            SetFilter();
        }



        private string mFilterText;


        private void SetFilter()
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(list.ItemsSource);
            if (searchBox.UseWildcards)
            {
                mFilterText = searchBox.GetRegularExpression();
                view.Filter = FilterMethodWithWildcards;
            }
            else
            {
                mFilterText = searchBox.Text;
                view.Filter = FilterMethod;
            }

                
        }


        private bool FilterMethod(object item)
        {
            //mFilterText = this.searchBox.Text;
            return (item.ToString().IndexOf(mFilterText, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private bool FilterMethodWithWildcards(object item)
        {
            return Regex.IsMatch(item.ToString(), mFilterText, RegexOptions.IgnoreCase);
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton tb = sender as ToggleButton;
            if ((bool)tb.IsChecked)
                this.chkUseWildcards.Visibility = Visibility.Visible;
            else
                this.chkUseWildcards.Visibility = Visibility.Collapsed;
        }
    }
}
