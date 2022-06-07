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
using ToolTip_DataGridTextColumn.Models;

namespace ToolTip_DataGridTextColumn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random random = new Random();

        public MainWindow()
        {
            InitializeComponent();
            dgExample.ItemsSource = CreatePeople();
            dgExample.Focus();
        }

        private List<Person> CreatePeople()
        {
            List<Person> people = new List<Person>();

            for (int i = 1; i <= 20; i++)
            {
                people.Add(new Person()
                {
                    Id = i,
                    FirstName = $"Firstname {i}",
                    LastName = $"Lastname {i}",
                    Age = random.Next(5, 66),
                    Adress = $"Street {i} {random.Next(1, 200)}, {random.Next(1000, 9999)} City, Austria"
                }); ;
            }

            return people;
        }

        private void DataGridCell_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is DataGridCell objDataGridCell)
            {
                if (objDataGridCell.Column.Header.ToString() != "Lastname" && objDataGridCell.Column.Header.ToString() != "Firstname")
                {
                    return;
                }

                Border objSelectedBorder = Utils.GetChildOfType<Border>(objDataGridCell);
                if (null == objSelectedBorder)
                {
                    return;
                }

                ToolTipService.SetInitialShowDelay(objSelectedBorder, 3000);
                ToolTipService.SetShowDuration(objSelectedBorder, 15000);
                ToolTipService.SetBetweenShowDelay(objSelectedBorder, 10000);

                objSelectedBorder.ToolTip = "The value of this column can be modified.";
            }
        }
    }
}
