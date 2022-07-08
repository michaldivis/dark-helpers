using System.Globalization;

namespace DarkHelpers.Maui;

public abstract class DarkConverterBase : IValueConverter
{
    public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException($"The {nameof(Convert)} method is not supported in {GetType().Name}.");
    }

    public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException($"The {nameof(ConvertBack)} method is not supported in {GetType().Name}.");
    }
}
