using Newtonsoft.Json;
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

namespace SingleExe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// The whole Tutorial was found here: https://www.youtube.com/watch?v=VQ7BziAyges
        /// 
        /// Add to the "%ApplicationName%.csproj" file (inside of the <Project> Tag):
        ///
        /// <Target Name="AfterResolveReferences">
        ///     <ItemGroup>
        ///         <EmbeddedResource Include = "@(ReferenceCopyLocalPaths)" Condition="'%(ReferenceCopyLocalPaths.Extension)' == '.dll'">
        ///         <LogicalName>%ReferenceCopyLocalPaths.DestinationSubDirectory)%(ReferenceCopyLocalPaths.Filename)%(ReferenceCopyLocalPaths.Extension)</LogicalName>
        ///     </EmbeddedResource>
        ///     </ItemGroup>
        /// </Target>
        /// 
        /// And take a look into the "App.xaml.cs"
        /// 
        /// </summary>

        public MainWindow()
        {
            InitializeComponent();
            SerializeObject();
        }

        private void SerializeObject()
        {
            var json = JsonConvert.SerializeObject(new { a = "string", b = 1 }, Formatting.Indented);
            demoTextBlock.Text = json;
        }
    }
}
