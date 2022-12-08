using System;
using System.Globalization;
using System.Windows;

namespace FMX.AccountsManager
{
    public class BoolToGridLengthConverter : ValueConverterBase<BoolToGridLengthConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? new GridLength(1, GridUnitType.Star) : new GridLength(0);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
