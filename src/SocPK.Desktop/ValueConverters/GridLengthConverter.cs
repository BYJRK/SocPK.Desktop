using System;
using System.Globalization;
using System.Windows;

namespace SocPK.Desktop.ValueConverters
{
    public class GridLengthConverter : BaseValueConverter
    {
        public double Total { get; set; }
        public bool IsReverse { get; set; }

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Total <= 0)
                throw new ArgumentException(value.ToString());
            if (value is double val)
            {
                var res = val / Total;
                if (IsReverse) res = 1 - res;
                return new GridLength(res, GridUnitType.Star);
            }
            throw new ArgumentException(value.ToString());
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
