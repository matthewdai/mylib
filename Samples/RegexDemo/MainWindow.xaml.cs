using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace RegexDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public class RegexViewModel : Abstracts.ViewModelBase
        {
            public List<string> OriginList { get; private set; }

            private string mSearchText;
            public string SearchText {
                get { return mSearchText; }
                set
                {
                    mSearchText = value;
                    NotifyPropertyChanged();

                    RegularExpression = WildcardsToRegular(value);
                }
            }


            private string mRegularExpression;
            public string RegularExpression
            {
                get { return mRegularExpression; }
                set
                {
                    mRegularExpression = value;
                    NotifyPropertyChanged();
                }
            }


            public RegexViewModel()
            {
                OriginList = new List<string>();
                OriginList.Add("Minghui Dai");
                OriginList.Add("Fred Dai");
            }


            private string WildcardsToRegular(string value)
            {
                bool hasQuestionChar = value.IndexOf('?') >= 0;
                bool hasStarChar = value.IndexOf('*') >= 0;

                if (hasQuestionChar && hasStarChar)
                    return "^" + Regex.Escape(value).Replace("\\?", ".").Replace("\\*", ".*") + "$";
                else if (hasStarChar)
                    return "^" + Regex.Escape(value).Replace("\\*", ".*") + "$";
                else if (hasQuestionChar)
                    return "^" + Regex.Escape(value).Replace("\\?", ".") + "$";

                return value;
            }


        }

        private RegexViewModel mViewModel;

        public MainWindow()
        {
            InitializeComponent();

            Init();
            this.DataContext = mViewModel;
        }

        private void Init()
        {
            mViewModel = new RegexViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //WildcardPattern
            filteredList.Items.Clear();

            var pattern = mViewModel.RegularExpression;

            foreach (var s in mViewModel.OriginList)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(s, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase))
                {
                    filteredList.Items.Add(s);
                }
            }
        }


   
    }
}
