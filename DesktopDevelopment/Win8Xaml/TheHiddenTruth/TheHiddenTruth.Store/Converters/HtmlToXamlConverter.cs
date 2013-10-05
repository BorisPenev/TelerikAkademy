using System;
using Windows.UI.Xaml.Data;
using Html2Xaml;

namespace HiddenTruth.Store.Converters
{
    public class HtmlToXamlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && !string.IsNullOrEmpty(value.ToString()))
            {
                return Html2XamlConverter.Convert2Xaml(value.ToString());
            }
            
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
