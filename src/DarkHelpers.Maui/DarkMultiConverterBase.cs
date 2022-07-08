using System.Globalization;

namespace DarkHelpers.Maui;

public abstract class DarkMultiConverterBase : IMultiValueConverter
{
    public virtual object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException($"The {nameof(Convert)} method is not supported in {GetType().Name}.");
    }

    public virtual object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException($"The {nameof(ConvertBack)} method is not supported in {GetType().Name}.");
    }
}
