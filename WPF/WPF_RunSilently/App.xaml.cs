using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPF_RunSilently.Model;

namespace WPF_RunSilently
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            /// ##################################################################################
            /// Validate CommandLine Arguments
            /// ##################################################################################
            string[] cmdArgs = Environment.GetCommandLineArgs();

            if (null != cmdArgs && cmdArgs.Length > 1)
            {
                /// ##################################################################################
                /// Do here your silent stuff:
                /// ##################################################################################

                Logging.LogParameters(cmdArgs);
                this.Shutdown();
            }
            else
            {
                /// ##################################################################################
                /// Create new MainWndow and start it:
                /// ##################################################################################

                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
        }
    }
}
