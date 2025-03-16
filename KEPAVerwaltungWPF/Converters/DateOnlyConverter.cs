using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace KEPAVerwaltungWPF.Converters;

public class DateOnlyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateTime dateTime)
        {
            return dateTime.ToString("dd.MM.yyyy");
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value;
    }
}