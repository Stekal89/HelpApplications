using Custom.UserControls.Models;
using Microsoft.Xaml.Behaviors; // Korrigierter Namespace
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

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiSelectionCombobox"/> class.
        /// </summary>
        public MultiSelectionCombobox()
        {
            InitializeComponent();
            if (DataContext is MultiSelectionViewModel viewModel)
            {
                _viewModel = viewModel;
            }
            else
            {
                _viewModel = new MultiSelectionViewModel();
                DataContext = _viewModel;
            }
        }

        /// <summary>
        /// Gets or sets the items source.
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                nameof(ItemsSource),
                typeof(List<MultiSelectItem>),
                typeof(MultiSelectionCombobox),
                new PropertyMetadata(null, OnItemsSourceChanged));

        /// <summary>
        /// Gets or sets the items source.
        /// </summary>
        public List<MultiSelectItem> ItemsSource
        {
            get => (List<MultiSelectItem>)GetValue(ItemsSourceProperty);
            set => SetValue(ItemsSourceProperty, value);
        }

        /// <summary>
        /// Called when items source is changed.
        /// </summary>
        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is MultiSelectionCombobox control && control._viewModel != null)
            {
                control._viewModel.SetItemsSource(e.NewValue as List<MultiSelectItem>);
            }
        }
    }

    public class MultiSelectionViewModel : INotifyPropertyChanged
    {
        private List<MultiSelectItem> _multiSelectItems = new List<MultiSelectItem>();
        private string _displayName = string.Empty;
        private bool _isDropDownOpen;

        /// <summary>
        /// Gets or sets the multi select items.
        /// </summary>
        public List<MultiSelectItem> MultiSelectItems
        {
            get => _multiSelectItems;
            set
            {
                _multiSelectItems = value ?? new List<MultiSelectItem>();
                OnPropertyChanged();
                UpdateDisplayName();
            }
        }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        public string DisplayName
        {
            get => _displayName;
            set
            {
                _displayName = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the drop down is open.
        /// </summary>
        public bool IsDropDownOpen
        {
            get => _isDropDownOpen;
            set
            {
                _isDropDownOpen = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Gets the toggle check command.
        /// </summary>
        public ICommand ToggleCheckCommand { get; }

        /// <summary>
        /// Gets the toggle drop down command.
        /// </summary>
        public ICommand ToggleDropDownCommand { get; }

        /// <summary>
        /// Gets the preview mouse left button down command.
        /// </summary>
        public ICommand PreviewMouseLeftButtonDownCommand { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiSelectionViewModel"/> class.
        /// </summary>
        public MultiSelectionViewModel()
        {
            ToggleCheckCommand = new RelayCommand(_ => UpdateDisplayName());
            ToggleDropDownCommand = new RelayCommand(_ => IsDropDownOpen = !IsDropDownOpen);
            PreviewMouseLeftButtonDownCommand = new RelayCommand(param =>
            {
                if (param is MouseButtonEventArgs e && e.OriginalSource.ToString() == "System.Windows.Controls.TextBoxView")
                {
                    IsDropDownOpen = !IsDropDownOpen;
                }
            });
        }

        /// <summary>
        /// Sets the items source.
        /// </summary>
        public void SetItemsSource(List<MultiSelectItem>? items)
        {
            MultiSelectItems = items ?? new List<MultiSelectItem>();
        }

        /// <summary>
        /// Updates the display name.
        /// </summary>
        public void UpdateDisplayName()
        {
            var selectedNames = MultiSelectItems?.Where(o => o.IsSelected)
                                               .Select(o => o.Name)
                                               .ToList() ?? new List<string>();
            DisplayName = selectedNames.Any()
                ? string.Join(", ", selectedNames)
                : string.Empty;
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Called when a property value changes.
        /// </summary>
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool>? _canExecute;

        /// <summary>
        /// Initializes a new instance of the <see cref="RelayCommand"/> class.
        /// </summary>
        public RelayCommand(Action<object> execute, Func<object, bool>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        public void Execute(object? parameter) => _execute(parameter);
    }

    /// <summary>
    /// Behavior to bind PreviewMouseLeftButtonDown event to a command.
    /// </summary>
    public class MouseLeftButtonDownBehavior : Behavior<UIElement>
    {
        /// <summary>
        /// Dependency property for the command to bind to the PreviewMouseLeftButtonDown event.
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register(
                nameof(Command),
                typeof(ICommand),
                typeof(MouseLeftButtonDownBehavior),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets the command to execute on PreviewMouseLeftButtonDown.
        /// </summary>
        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.PreviewMouseLeftButtonDown += OnPreviewMouseLeftButtonDown;
        }

        /// <summary>
        /// Called when the behavior is being detached from its AssociatedObject.
        /// </summary>
        protected override void OnDetaching()
        {
            AssociatedObject.PreviewMouseLeftButtonDown -= OnPreviewMouseLeftButtonDown;
            base.OnDetaching();
        }

        private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Command != null && Command.CanExecute(e))
            {
                Command.Execute(e);
            }
        }
    }
}

namespace Custom.UserControls.Models
{
    public class MultiSelectItem
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        public int Id { get; set; } = 0;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether this item is selected.
        /// </summary>
        public bool IsSelected { get; set; } = false;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MultiSelectItem() { }

        /// <summary>
        /// Full constructor.
        /// </summary>
        public MultiSelectItem(int id, string name, bool isSelected)
        {
            Id = id;
            Name = name;
            IsSelected = isSelected;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MultiSelectItem"/> class.
        /// </summary>
        public MultiSelectItem(string name, bool isSelected)
        {
            Name = name;
            IsSelected = isSelected;
        }
    }
}