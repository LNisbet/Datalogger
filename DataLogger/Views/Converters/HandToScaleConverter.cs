using DataLogger.Models;
using System.Globalization;
using System.Windows.Data;

namespace DataLogger.Views.Converters
{
    public class HandToScaleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is FingerStatistics.Hand hand && hand == FingerStatistics.Hand.Right)
            {
                return -1;
            }
            return 1; // Default to no mirroring if value is not a bool
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double scaleX && scaleX < 0)
            {
                return FingerStatistics.Hand.Right;
            }
            return FingerStatistics.Hand.Left;
        }
    }
}
