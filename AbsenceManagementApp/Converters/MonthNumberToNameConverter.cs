using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace AbsenceManagementApp.Converters
{
    public class MonthNumberToNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int monthNumber && monthNumber >= 1 && monthNumber <= 12)
            {
                return new DateTime(2023, monthNumber, 1).ToString("MMMM", CultureInfo.CurrentCulture);
            }
            
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

