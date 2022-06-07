using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

using TreeViewExample.Model;

namespace TreeViewExample
{
    /// <summary>
    /// Für ein Multibinding benötigen wir zwingend einen IMultiValueConverter, in diesem Fall habe ich bereits den “PersonSubItemConverter” angegeben. In diesem werden wir die Unterlisten Listen entsprechend “konvertieren” für die Darstellung:
    /// </summary>
    public class PersonSubitemConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<Beer> beers = (ObservableCollection<Beer>)values[0];
            ObservableCollection<Book> books = (ObservableCollection<Book>)values[1];
            List<object> items = new List<object>();

            FolderItem folderItemThen = new FolderItem() { Name = "Lieblingsbiere", Items = beers };
            FolderItem folderItemElse = new FolderItem() { Name = "Lieblingsbücher", Items = books };

            items.Add(folderItemThen);
            items.Add(folderItemElse);

            return items;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("Cannot be done!");
        }
    }
}
