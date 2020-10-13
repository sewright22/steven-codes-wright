using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace DiabetesFoodJournal.Converters
{
    public class RoundValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var decimalValue = (decimal)value;

            var tempValue = decimalValue * 100;
            var newStep = Math.Round(tempValue);

            return newStep / 100;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var decimalValue = (double)value;

            var tempValue = decimalValue * 100;
            var newStep = Math.Round(tempValue);

            return System.Convert.ToDecimal(newStep / 100);
        }
    }
}
