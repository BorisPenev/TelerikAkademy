﻿using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace CinemaReserve.WpfClient.Converters
{
    public class CountToVisibilityConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = Visibility.Visible;
            if (parameter == null)
            {
                if (value != null && value is ICollection && (value as ICollection).Count == 0)
                    result = Visibility.Collapsed;
            }
            else if (parameter.ToString() == "Inverse")
            {
                if (value != null && value is ICollection && (value as ICollection).Count > 0)
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