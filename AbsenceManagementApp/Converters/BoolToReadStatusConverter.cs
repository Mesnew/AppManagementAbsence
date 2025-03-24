using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace AbsenceManagementApp.Converters
{
    public class BoolToReadStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isRead)
            {
                return isRead ? "Lu" : "Non lu";
            }
            
            return "Non lu";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

