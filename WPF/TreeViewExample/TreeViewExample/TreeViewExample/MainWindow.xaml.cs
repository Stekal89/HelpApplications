using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

using TreeViewExample.Model;

namespace TreeViewExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Person> _persons = new ObservableCollection<Person>();

        public MainWindow()
        {
            InitializeComponent();
            InitData();
            this.DataContext = this;
        }

        private void InitData()
        {
            Person andy = new Person() { Name = "Andy", Age = "39"};
            Person kiwi = new Person() { Name = "Kiwi", Age = "39" };
            Person ebe = new Person() { Name = "Ebe", Age = "39" };

            andy.FavoriteBeers.Add(new Beer() { Name = "Ötti", Alcolhol = "4,7" });
            andy.FavoriteBeers.Add(new Beer() { Name = "Jever Fun", Alcolhol = "0,5" });
            andy.FavoriteBooks.Add(new Book() { Name = "Faust", Isbn = "1507547267" });
            andy.FavoriteBooks.Add(new Book() { Name = "Im Westen nichts Neues", Isbn = "3462046335" });

            kiwi.FavoriteBeers.Add(new Beer() { Name = "Meisterpilz", Alcolhol = "4,8" });
            kiwi.FavoriteBeers.Add(new Beer() { Name = "Krombacher", Alcolhol = "4,9" });
            kiwi.FavoriteBooks.Add(new Book() { Name = "Wie wird man Millionär", Isbn = "3458334163" });
            kiwi.FavoriteBooks.Add(new Book() { Name = "Mathematik für Ingenieure", Isbn = "3658217457" });

            ebe.FavoriteBeers.Add(new Beer() { Name = "Tegernsee Gold", Alcolhol = "5,1" });
            ebe.FavoriteBeers.Add(new Beer() { Name = "Aktien Hell", Alcolhol = "5,2" });
            ebe.FavoriteBeers.Add(new Beer() { Name = "Augustiner", Alcolhol = "5,4" });
            ebe.FavoriteBooks.Add(new Book() { Name = "Die Trilogie der Induktivitäten", Isbn = "3934350304" });
            ebe.FavoriteBooks.Add(new Book() { Name = "Glockenankermotoren", Isbn = "3937889504" });

            _persons.Add(andy);
            _persons.Add(kiwi);
            _persons.Add(ebe);

        }

        public ObservableCollection<Person> Persons { get => _persons; set => _persons = value; }
    }
}
