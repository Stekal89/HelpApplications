using MVVM_Example.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MVVM_Example.ViewModel
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        #region Properties

        private string _searchText;

        /// <summary>
        /// Search Value which should be filtered.
        /// </summary>
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));

                // Call here the functionality to filter the values
                FilterPeoples();
            }
        }

        /// <summary>
        /// Loaded People - Full list
        /// </summary>
        public ObservableCollection<Person> People { get; set; }

        /// <summary>
        /// Filtered People
        /// </summary>
        public ObservableCollection<Person> FilteredPeople { get; set; }

        #endregion // Properties

        #region Constructors

        /// <summary>
        /// Default-Constructor
        /// </summary>
        public PersonViewModel()
        {
            // Sample data that is normally loaded from a database
            People = new ObservableCollection<Person>
            {
                new Person { Name = "Anna", Age = 25, Gender = "Weiblich" },
                new Person { Name = "Ben", Age = 30, Gender = "Männlich" },
                new Person { Name = "Clara", Age = 22, Gender = "Weiblich" }
            };
            
            FilteredPeople = new ObservableCollection<Person>(People);
        }

        #endregion // Constructors

        #region Functions

        /// <summary>
        /// Function for filtering persons -> only show matching persons in the list box.
        /// </summary>
        private void FilterPeoples()
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                // If the search text is empty, show all persons
                FilteredPeople = new ObservableCollection<Person>(People);
            }
            else
            {
                // Filter the list, similar to SQL LIKE '%SearchText%'
                FilteredPeople = new ObservableCollection<Person>(People.Where(p => p.Name.IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0));
            }

            // Notify me that the filtered list has changed
            OnPropertyChanged(nameof(FilteredPeople));
        }

        #endregion // Functions

        #region Event Handlers

        /// <summary>
        /// Property Changed Event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion // Events
    }
}
