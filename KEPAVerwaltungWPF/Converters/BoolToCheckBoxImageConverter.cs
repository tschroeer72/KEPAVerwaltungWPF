using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace KEPAVerwaltungWPF.Converters;

public class BoolToCheckBoxImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool bolOK)
            {
                string imagePath = bolOK switch
                {
                    false => "pack://application:,,,/Images/checkbox_unchecked_24.png",
                    true => "pack://application:,,,/Images/checkbox_24.png"
                };
                return new BitmapImage(new Uri(imagePath));
            }

            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
}