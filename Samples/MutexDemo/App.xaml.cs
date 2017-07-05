using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
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
            CheckInstance();
        }


        static void CheckInstance()
        {
            using (SingleProgramInstance spi = new SingleProgramInstance("{4173DDF2-74F4-4927-9D35-9B826E916601}"))
            {
                if (spi.IsSingleInstance)
                {
                    Start();
                }
                else
                {
                    spi.RaiseOtherProcess();
                    spi.Dispose();
                    Application.Current.Shutdown();
                }
            }

        }



        static void Start() {
            //var process = Process.GetCurrentProcess();

            var win = new MainWindow();
            win.Show();
        }


    }
}
