using System;
using System.Globalization;
using System.Windows.Data;

namespace DarkHelpers.WPF
{
    public abstract class OneWayMultiConverterBase : IMultiValueConverter
    {
        public abstract object Convert(object[] values, Type targetType, object parameter, CultureInfo culture);

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException($"{GetType().Name} is a one way multi converter only.");
        }
    }
}
