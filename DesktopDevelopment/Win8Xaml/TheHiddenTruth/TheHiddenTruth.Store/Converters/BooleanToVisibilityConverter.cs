using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace TheHiddenTruth.Store.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Visibility result = Visibility.Visible;
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

        public object ConvertBack(object value, Type targetType, object parameter, string language)
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
    }
}
