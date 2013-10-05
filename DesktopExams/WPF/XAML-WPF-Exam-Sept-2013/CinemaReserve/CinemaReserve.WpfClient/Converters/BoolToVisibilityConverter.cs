using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace CinemaReserve.WpfClient.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility result = Visibility.Collapsed;
            if (parameter == null)
            {
                if (value != null && (bool)value)
                    result = Visibility.Visible;
            }
            else if (parameter.ToString() == "Inverse")
            {
                if (value != null && (bool)value)
                    result = Visibility.Collapsed;
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = false;
            if (parameter == null)
            {
                if (value != null && (Visibility)value == Visibility.Visible)
                    result = true;
            }
            else if (parameter.ToString() == "Inverse")
            {
                if (value != null && (Visibility)value == Visibility.Visible)
                    result = false;
            }
            return result;
        }

        #endregion
    }
}
