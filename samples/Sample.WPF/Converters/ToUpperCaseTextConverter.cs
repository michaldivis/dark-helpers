﻿using DarkHelpers.WPF;
using System;
using System.Globalization;

namespace Sample.WPF.Converters;
public class ToUpperCaseTextConverter : DarkConverterBase
{
    public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string text)
        {
            return text.ToUpper();
        }

        return value;
    }
}
