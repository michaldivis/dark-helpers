using System;
using System.Globalization;
using Xamarin.Forms;

namespace DarkHelpers.XF
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
