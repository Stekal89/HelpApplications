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
using WPF_RunSilently.Model;

namespace WPF_RunSilently
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// ##################################################################################
        ///             !!!Important!!!
        /// Take a look into the App.xaml and into the App.xaml.cs files
        /// ##################################################################################
        
        public MainWindow()
        {
            InitializeComponent();
            Logging.LogFromMainWindow();
        }
    }
}
