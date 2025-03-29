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
            MultiSelectionVM.MultiSelectItems = new List<MultiSelectItem>();
            for (int i = 0; i < _data.Rows.Count; i++)
            {
                MultiSelectionVM.MultiSelectItems.Add(new MultiSelectItem
                {
                    Id = (int)_data.Rows[i]["Id"],
                    Name = $"{_data.Rows[i]["FirstName"]} {_data.Rows[i]["LastName"]}",
                    IsSelected = true
                });
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
