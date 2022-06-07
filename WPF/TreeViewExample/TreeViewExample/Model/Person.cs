using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeViewExample.Model
{
    public class Person
    {
        private string _name = "";
        private string _age = "";

        public string Name { get => _name; set => _name = value; }
        public string Age { get => _age; set => _age = value; }
        public ObservableCollection<Beer> FavoriteBeers { get => _favoriteBeers; set => _favoriteBeers = value; }
        public ObservableCollection<Book> FavoriteBooks { get => _favoriteBooks; set => _favoriteBooks = value; }

        private ObservableCollection<Beer> _favoriteBeers = new ObservableCollection<Beer>();
        private ObservableCollection<Book> _favoriteBooks = new ObservableCollection<Book>();
    }
}
