using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace DataLogger.Views.Converters
{
    public class BooleanToScaleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isMirrored)
            {
                return isMirrored ? -1 : 1; // -1 for mirrored, 1 for normal
            }
            return 1; // Default to no mirroring if value is not a bool
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double scaleX)
            {
                return scaleX < 0; // Return true if mirrored, false if not
            }
            return false;
        }
    }
}
