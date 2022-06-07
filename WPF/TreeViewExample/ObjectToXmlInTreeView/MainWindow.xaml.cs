using ObjectToXmlInTreeView.Models;
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

namespace ObjectToXmlInTreeView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Category Category { get; set; } = new Category(true);
        public MainWindow()
        {
            InitializeComponent();
            //XMLFileToTreeViewMapper xmlMapper = new XMLFileToTreeViewMapper(@"..\..\..\LoadXMLFromFile\TestFiles\6p0_Muster.xml");
            //xmlMapper.LoadXml(tvTreeView);

            XMLToTreeViewMapper xmlMapper = new XMLToTreeViewMapper();
            xmlMapper.ConvertObjectToXML(Category, tvTreeView);
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Category = new Category();
            Category.CategoryName = "Produce";
            Category.SubCategories = new List<SubCategory>()
            {
                new SubCategory()
                {
                    SubCategoryName = "Computer",
                    Items = new List<Item>()
                    {
                        new Item()
                        {
                            ItemName = "CPU"
                        },
                        new Item()
                        {
                            ItemName = "Mainboard"
                        }
                    }
                },
                new SubCategory()
                {
                    SubCategoryName = "Periphery",
                    Items = new List<Item>()
                    {
                        new Item()
                        {
                            ItemName = "Mouse"
                        },
                        new Item()
                        {
                            ItemName = "Keyboard"
                        },
                        new Item()
                        {
                            ItemName = "Box"
                        }
                    }
                }
            };

            XMLToTreeViewMapper xmlMapper = new XMLToTreeViewMapper();
            tvTreeView.Items.Clear();
            xmlMapper.ConvertObjectToXML(Category, tvTreeView);
        }
    }
}
