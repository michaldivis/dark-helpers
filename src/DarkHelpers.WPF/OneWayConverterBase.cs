using System;
using System.Globalization;
using System.Windows.Data;

namespace DarkHelpers.WPF
{
    public abstract class OneWayConverterBase : IValueConverter
    {
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException($"{GetType().Name} is a one way converter only.");
        }
    }
}
