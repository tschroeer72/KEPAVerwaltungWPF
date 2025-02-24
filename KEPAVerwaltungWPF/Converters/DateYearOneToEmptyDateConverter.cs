using System.Globalization;
using System.Windows.Data;

namespace KEPAVerwaltungWPF.Converters;

public class DateYearOneToEmptyDateConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        try
        {
            if (value != null)
            {
                DateTime valDate = (DateTime)value;
                DateTime YearOneDateTime = new();
                if (valDate.ToShortDateString().Equals(YearOneDateTime.ToShortDateString()))
                {
                    return string.Empty;
                }

                return valDate;
            }
            else return string.Empty;
        }
        catch (Exception)
        {
            return string.Empty;
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value ?? new DateTime();
    }
}