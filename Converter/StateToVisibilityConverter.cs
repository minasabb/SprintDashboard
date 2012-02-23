using System;
using System.Collections;
using System.Globalization;
using System.Windows;
using System.Linq;
using System.Windows.Data;

namespace TextDashboard.Converter
{
    public class StateToVisibilityConverter:IValueConverter
    {
        public static StateToVisibilityConverter Instance { get { return _instance; } }
        private static readonly StateToVisibilityConverter _instance = new StateToVisibilityConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            if (parameter == null)
                throw new ArgumentNullException("parameter");

            var enumValues = parameter as Array;
            var state =  value.ToString();
            if (enumValues != null)
            {
                if (enumValues.Cast<object>().Any(enumValue => state == enumValue.ToString()))
                {
                    return Visibility.Visible;
                }
            }

            return  Visibility.Collapsed;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    
    }
}
