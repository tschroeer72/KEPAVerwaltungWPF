using System.Globalization;
using System.Windows;
using System.Windows.Data;
using KEPAVerwaltungWPF.Enums;

namespace KEPAVerwaltungWPF.Converters;

public class MagnifyEnumToMagnifyTypeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is MagnifyType val)
        {
            switch (val)
            {
                case MagnifyType.Circle:
                    return "Circle";
                case MagnifyType.Rectangle:
                    return "Rectangle";
            }
        }

        return "Circle";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}