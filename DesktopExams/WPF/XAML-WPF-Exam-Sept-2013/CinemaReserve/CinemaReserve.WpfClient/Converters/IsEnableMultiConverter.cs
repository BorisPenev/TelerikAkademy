using System;
using System.Linq;
using System.Windows.Data;

namespace CinemaReserve.WpfClient.Converters
{
    public class IsEnableMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            for (int i = 0; i < value.Count(); i++)
            {
                if (value[i] is int && (int)value[i] == -1) //SelectedIndex
                    return false;
                else if (value[i] is bool && (bool)value[i]) //validation
                    return false;
                else if (value[i] is string && string.IsNullOrEmpty(value[i].ToString())) //RequestString
                    return false;
            }
            return true;
        }

        public object[] ConvertBack(object value, Type[] targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
