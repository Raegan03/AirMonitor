using AirMonitor.Data;
using System;
using System.Globalization;

using Xamarin.Forms;

namespace AirMonitor.Converters
{
    class AddressToCityCountryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Address address)) return value;
            return $"{address.City} {address.Country}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
