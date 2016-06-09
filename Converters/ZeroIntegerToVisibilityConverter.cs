using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Simple_Stream_UWP.Converters
{
    public class ZeroIntegerToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int val = 0;

            if (int.TryParse(value.ToString(), out val))
                return val > 0 ? Visibility.Visible : Visibility.Collapsed;
            else
            {
                Debug.WriteLine($"Converter tried to parse string:{value.ToString()} but failed.");
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
