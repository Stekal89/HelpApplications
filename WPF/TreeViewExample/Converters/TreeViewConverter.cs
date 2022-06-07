using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TreeViewExample.Converters
{
    class TreeViewConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (null != values)
            {
                if (values.Length > 1)
                {
                    if (null != values[1] && !string.IsNullOrEmpty(values[1].ToString()))
                    {
                        return $"{values[0]}: \"{values[1]}\"";
                    }
                    else
                    {
                        return $"{values[0]}";
                    }
                }
                else
                {
                    return $"{values[0]}";
                }
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}