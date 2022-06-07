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
using TreeViewExample.Models;
using TreeViewExample.UserControls;

namespace TreeViewExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Category Category { get; set; } = new Category(true);
        public MainWindow()
        {
            //Category = new Category(true);
            InitializeComponent();
            
            CreateTree();

            CreateTreeDynamic();

            //Category category = new Category(true);
            //tvCustom3.ObjectToVisualize = new ObjectInTreeView() { ObjectToVisualize = category };
            tvCustom3.ObjectToVisualize = Category;

            Category = new Category(true);
            Category.CategoryName = "MyTest";
            tvCustom3.ObjectToVisualize = Category;
        }

        private void CreateTree()
        {
            Category category = new Category(true);

            TreeViewItem tv1 = new TreeViewItem()
            {
                Header = category.CategoryName
            };
            TreeViewItem tv2 = new TreeViewItem()
            {
                Header = category.SubCategories[0].SubCategoryName
            };
            TreeViewItem tv3 = new TreeViewItem()
            {
                Header = category.SubCategories[0].Items[0].ItemName
            };

            tv1.Items.Add(tv2);
            tv2.Items.Add(tv3);

            tvCustom1.Items.Add(tv1);
        }

        private void CreateTreeDynamic()
        {
            Category category = new Category(true);

            TreeViewItem tvFirstLevel = new TreeViewItem()
            {
                Header = category.CategoryName
            };

            foreach (var subCategory in category.SubCategories)
            {
                TreeViewItem tviSubCategory = new TreeViewItem();
                tviSubCategory.Header = subCategory.SubCategoryName;

                foreach (var item in subCategory.Items)
                {
                    TreeViewItem tviItem = new TreeViewItem();
                    tviItem.Header = item.ItemName;

                    tviSubCategory.Items.Add(tviItem);
                }

                tvFirstLevel.Items.Add(tviSubCategory);
            }

            tvCustom2.Items.Add(tvFirstLevel);
        }
    }
}
