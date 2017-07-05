using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace MutexDemo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            Start();
        }


        static void Start()
        {
            using (SingleProgramInstance spi = new SingleProgramInstance("{4173DDF2-74F4-4927-9D35-9B826E916601}"))
            {
                if (spi.IsSingleInstance)
                {
                    var win = new MainWindow();
                    win.Show();
                    spi.Dispose();
                }
                else
                {
                    spi.RaiseOtherProcess();
                    spi.Dispose();
                    Application.Current.Shutdown();
                }
            }

        }


    }
}
