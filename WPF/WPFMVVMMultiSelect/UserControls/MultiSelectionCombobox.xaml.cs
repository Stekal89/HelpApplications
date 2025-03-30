using Custom.UserControls.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Custom.UserControls
{
    public partial class MultiSelectionCombobox : UserControl
    {
        private readonly MultiSelectionViewModel _viewModel;

        public MultiSelectionCombobox()
        {
            InitializeComponent();
            _viewModel = new MultiSelectionViewModel();
            DataContext = _viewModel;
        }

        private void MultiSelectItem_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (sender is FrameworkElement element && element.DataContext is MultiSelectItem item)
            {
                if (DataContext is MultiSelectionViewModel vm)
                {
                    // manually call Click logic from the ViewModel
                    vm.SelectItemClick(item); 

                    // Check if all items except the first one are selected
                    vm.MultiSelectItems[0].IsSelected = vm.MultiSelectItems.Skip(1).All(i => i.IsSelected);

                    e.Handled = true;
                }
            }
        }
    }

    public class MultiSelectionViewModel : INotifyPropertyChanged
    {
        private MultiSelectItemObservableCollection _multiSelectItems = new MultiSelectItemObservableCollection() { new MultiSelectItem("Select all", false) };

        /// <summary>
        /// Gets or sets the collection of multi-select items.
        /// </summary>
        public MultiSelectItemObservableCollection MultiSelectItems
        {
            get => _multiSelectItems;
            set
            {
                if (_multiSelectItems != value)
                {
                    _multiSelectItems = value;

                    // Add "Select all" option as the first item
                    if (_multiSelectItems[0].Name != "Select all")
                    {
                        _multiSelectItems.Insert(0, new MultiSelectItem("Select all"));
                    }

                    DisplayName = $"{_multiSelectItems.Count} Items selected";
                    OnPropertyChanged();
                }
            }
        }

        private string _displayName = string.Empty;

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName
        {
            get => _displayName;
            set
            {
                if (_displayName != value)
                {
                    _displayName = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isDropDownOpen;

        /// <summary>
        /// Gets or sets a value indicating whether the dropdown is open.
        /// </summary>
        public bool IsDropDownOpen
        {
            get => _isDropDownOpen;
            set
            {
                if (_isDropDownOpen != value)
                {
                    _isDropDownOpen = value;
                    OnPropertyChanged();
                }
            }
        }

        public MultiSelectionViewModel()
        {
            // Add "Select all" option as the first item
            _multiSelectItems.Add(new MultiSelectItem("Select all"));
        }

        /// <summary>
        /// Handles the item click event to toggle selection.
        /// </summary>
        /// <param name="parameter">The clicked item.</param>
        public void SelectItemClick(object? parameter)
        {
            if (parameter is not MultiSelectItem item)
                return;

            if (item.Name == "Select all")
            {
                bool selectAll = !item.IsSelected;
                foreach (var multiSelectItem in _multiSelectItems)
                {
                    multiSelectItem.IsSelected = selectAll;
                }
            }
            else
            {
                // Toggle the status manually (since the click was suppressed)
                item.IsSelected = !item.IsSelected;
            }

            // Keep the dropdown open
            IsDropDownOpen = true;

            // Update display
            DisplayName = $"{MultiSelectItems.Count(i => i.IsSelected)} Items selected";
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Predicate<object?>? _canExecute;

        public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <summary>
        /// Determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public bool CanExecute(object? parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="parameter">Data used by the command.</param>
        public void Execute(object? parameter)
        {
            _execute(parameter);
        }

        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}

namespace Custom.UserControls.Models
{
    public class MultiSelectItem : INotifyPropertyChanged
    {
        private string _name = string.Empty;

        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isSelected = false;

        /// <summary>
        /// Gets or sets a value indicating whether the item is selected.
        /// </summary>
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged();
                }
            }
        }

        public MultiSelectItem() { }
        public MultiSelectItem(string name, bool isSelected = false)
        {
            Name = name;
            IsSelected = isSelected;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class MultiSelectItemObservableCollection : ObservableCollection<MultiSelectItem>
    {
        protected override void ClearItems()
        {
            base.ClearItems();

            // Custom logic after clearing items
            // Add "Select all" option as the first item
            if (this.Count == 0 || this[0].Name != "Select all")
            {
                this.Insert(0, new MultiSelectItem("Select all"));
            }
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);

            if (this.Count > 0)
            {
                // Check if all items except the first one are selected
                this[0].IsSelected = this.Skip(1).All(i => i.IsSelected);
            }
        }
    }
}