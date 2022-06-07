using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using WpfDataGrid.Model;

namespace WpfDataGrid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private List<Article> articles = new List<Article>();
        private Random rnd = new Random();
        private int count;
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates fake data for testing.
        /// </summary>
        private List<Article> CreateFakeData()
        {
            List<Article> articles = new List<Article>();

            for (int i = 0; i < 10; i++)
            {
                count = rnd.Next(1, 300);
                articles.Add(new Article(Group.Baumax, $"Wood - {i}", count));
                count = rnd.Next(1, 300);
                articles.Add(new Article(Group.Obi, $"Wood - {i}", count));
                count = rnd.Next(1, 300);
                articles.Add(new Article(Group.Hervis, $"Sport Shoes - {i}", count));
                count = rnd.Next(1, 300);
                articles.Add(new Article(Group.Intersport, $"Sport Shoes - {i}", count));
            }
            for (int i = 0; i < 10; i++)
            {
                count = rnd.Next(1, 300);
                articles.Add(new Article(Group.Baumax, $"Metal - {i}", count));
                count = rnd.Next(1, 300);
                articles.Add(new Article(Group.Obi, $"Metal - {i}", count));
                count = rnd.Next(1, 300);
                articles.Add(new Article(Group.Hervis, $"Ski - {i}", count));
                count = rnd.Next(1, 300);
                articles.Add(new Article(Group.Intersport, $"Ski - {i}", count));
            }
            for (int i = 0; i < 10; i++)
            {
                count = rnd.Next(1, 300);
                articles.Add(new Article(Group.Baumax, $"WoodWood - {i}", count));
                count = rnd.Next(1, 300);
                articles.Add(new Article(Group.Obi, $"Wood - {i}", count));
                count = rnd.Next(1, 300);
                articles.Add(new Article(Group.Hervis, $"Sport Shoes - {i}", count));
                count = rnd.Next(1, 300);
                articles.Add(new Article(Group.Intersport, $"Sport Shoes - {i}", count));
            }
            return articles;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /*
                S O L U T I O N   1 :


            List<Article> articles = CreateFakeData();

            var cvsObi = this.FindResource("cvsObi") as CollectionViewSource;
            var cvsBaumax = this.FindResource("cvsBaumax") as CollectionViewSource;
            var cvsHervis = this.FindResource("cvsHervis") as CollectionViewSource;
            var cvsIntersport = this.FindResource("cvsIntersport") as CollectionViewSource;
            cvsObi.Source = articles;
            cvsBaumax.Source = articles;
            cvsHervis.Source = articles;
            cvsIntersport.Source = articles;

            */

            // S O L U T I O N   2 :

            this.DataContext = CreateFakeData();
        }

        #region DataGrid Filter

        private void ShowObi(object sender, FilterEventArgs e)
        {
            Article article = e.Item as Article;
            if (article != null)
                e.Accepted = (article.Group == Group.Obi);
        }

        private void ShowBaumax(object sender, FilterEventArgs e)
        {
            Article article = e.Item as Article;
            if (article != null)
                e.Accepted = (article.Group == Group.Baumax);
        }

        private void ShowHervis(object sender, FilterEventArgs e)
        {
            Article article = e.Item as Article;
            if (article != null)
                e.Accepted = (article.Group == Group.Hervis);
        }

        private void ShowIntersport(object sender, FilterEventArgs e)
        {
            Article article = e.Item as Article;
            if (article != null)
                e.Accepted = (article.Group == Group.Intersport);
        }

        #endregion
    }
}
