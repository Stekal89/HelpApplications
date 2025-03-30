using Custom.UserControls;
using Custom.UserControls.Models;
using System.ComponentModel;
using System.Data;
using System.Runtime.CompilerServices;
using WPFMVVMMultiSelect.Models;

namespace WPFMVVMMultiSelect.ViewModels
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        #region Property-Helpers

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value)) return false;
            storage = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return true;
        }

        #endregion // Property-Helpers

        public MultiSelectionViewModel MultiSelectionVM { get; } = new MultiSelectionViewModel();

        private DataTable _data;

        public DataTable Data
        {
            get => _data;
            set
            {
                if (_data != value)
                {
                    _data = value;
                    OnPropertyChanged();
                }
            }
        }

        public PersonViewModel()
        {
            TestData testData = new TestData();
            _data = testData.Table;

            // Initialize MultiSelectItems with test data
            //MultiSelectionVM.MultiSelectItems = new System.Collections.ObjectModel.ObservableCollection<MultiSelectItem>();

            MultiSelectionVM.MultiSelectItems.Clear();

            //var items = new List<MultiSelectItem>();
            for (int i = 0; i < _data.Rows.Count; i++)
            {
                //items.Add(new MultiSelectItem
                MultiSelectionVM.MultiSelectItems.Add(new MultiSelectItem
                {
                    Name = $"{_data.Rows[i]["FirstName"]} {_data.Rows[i]["LastName"]}",
                    IsSelected = true
                });
            }
            // Add the Items to the MultiSelectionVM
            // To be sure, that the "Select all" Entry is the first one
            // We have to set it at this way

            //MultiSelectionVM.MultiSelectItems.Add(new MultiSelectItem { Name = "Select all", IsSelected = true });
            //MultiSelectionVM.SetItemsSource(items);

            //MultiSelectionVM.MultiSelectItems = items;
 
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
