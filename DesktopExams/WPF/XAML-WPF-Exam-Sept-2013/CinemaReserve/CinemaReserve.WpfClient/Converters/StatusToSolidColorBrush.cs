using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace CinemaReserve.WpfClient.Converters
{
    public class StatusToSolidColorBrush : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SolidColorBrush result = new SolidColorBrush(Colors.Transparent);

            if (value != null)
            {
                if (value.ToString() == "free")
                {
                    result = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    result = new SolidColorBrush(Colors.Red);
                }
            }
                

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color result = Colors.Transparent;

            if (value != null)
                result = ((SolidColorBrush)value).Color;

            return result;
        }

        #endregion
    }
}
