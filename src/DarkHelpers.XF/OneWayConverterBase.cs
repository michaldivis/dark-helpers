using System;
using System.Globalization;
using Xamarin.Forms;

namespace DarkHelpers.XF
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
