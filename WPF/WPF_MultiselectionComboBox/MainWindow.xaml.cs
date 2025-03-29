using System.Collections.Generic;
using System.Windows;
using WPF_MultiselectionComboBox.Models;

namespace WPF_MultiselectionComboBox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Country> objCountryList;

        public MainWindow()
        {
            InitializeComponent();
            objCountryList = new List<Country>();
            AddElementsInList();
            List<CBXObject> liOfCBXObjects = new List<CBXObject>();
            objCountryList.ForEach(x => liOfCBXObjects.Add(x));
            cbxMultiBox.BindObjectToDropDown(liOfCBXObjects);
        }

        private void AddElementsInList()
        {
            objCountryList.Add( new Country() 
            {
                ObjectId   = 10,
                ObjectName = "India"
            });
            objCountryList.Add(new Country()
            {
                ObjectId   = 11,
                ObjectName = "Pakistan"
            });
            objCountryList.Add(new Country()
            {
                ObjectId   = 12,
                ObjectName = "America"
            });
            objCountryList.Add(new Country()
            {
                ObjectId   = 13,
                ObjectName = "U.K"
            });
            objCountryList.Add(new Country()
            {
                ObjectId = 14,
                ObjectName = "Arab"
            });
        }
    }
}
