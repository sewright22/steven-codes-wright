using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace DiabetesFoodJournal.Converters
{
    public class MinimumValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var decimalValue = value as decimal?;

            if (decimalValue.HasValue==false || decimalValue.Value<=0)
            {
                return 1000;
            }
            else
            {
                return decimalValue.Value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
